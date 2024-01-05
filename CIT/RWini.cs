using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;


namespace CIT
{
    /// <summary>
    /// 公用 INI 文件读写类。
    /// </summary>
    public class RWIni
    {
        private string path = MainForm.BaseDir;
        private string exe = Assembly.GetExecutingAssembly().GetName().Name;
        private bool iniExist;
        // 判断 ini 文件是否存在。
        public bool IniExist { get { return this.iniExist; } }
        private Encoding iniEncoding;
        public Encoding IniEncoding { get { return this.iniEncoding; } }
        // 调用 Win32API 读写 ini 文件。
        // C# 默认 Charset 为 Ansi 字符集，Auto 同机制，可省略。
        // 这里只考虑 Windows 默认记事本支持的四种编码选项。
        // 使用字符串传入传出，函数默认支持 Windows 默认的 gb 编码(ANSI)或 UTF16LE(Unicode)。
        [DllImport("kernel32")]
        static extern long WritePrivateProfileString(string section, string key, string value, string filePath);
        [DllImport("kernel32")]
        static extern int GetPrivateProfileString(string section, string key, string defaultStr, StringBuilder outStr, int size, string filePath);
        // 这里保留另一种重载，但并不支持读取剩余编码(UTF8 UTF16BE)
        [DllImport("kernel32")]
        static extern int GetPrivateProfileString(byte[] section, byte[] key, byte[] defaultStr, byte[] outVal, int size, string filePath);

        public RWIni()
        {
            // 定义了 ini 文件的命名。
            this.path = this.path + this.exe + "_settings.ini";
            // 因为 ini 文件名是固定的，所以先判断文件存在否。
            if (File.Exists(this.path))
            {
                this.iniExist = true;
                this.GetEncoding();
            }
            else
            {
                this.iniExist = false;
            }
        }

        /// <summary>
        /// 读取 ini 文件键值。
        /// </summary>
        /// <param name="key">需要读取的键名字符串</param>
        /// <param name="section">需要读取的节名字符串</param>
        /// <returns>读取到的值字符串，为空返回零长字符串</returns>
        public string Read(string key, string section)
        {
            var RetVal = new StringBuilder(1024);
            GetPrivateProfileString(section, key, "", RetVal, 1024, this.path);
            return RetVal.ToString();
            // 这里对应另一种重载，以 byte[] 传递。
            /* byte[] buffer = new byte[1024];
            int buff_count = GetPrivateProfileString(Str2Byte(section, this.iniEncoding), Str2Byte(key, this.iniEncoding), Str2Byte("", this.iniEncoding), buffer, 1024, path);
            return Encoding.UTF8.GetString(buffer, 0, buff_count); */
        }

        /// <summary>
        /// 写入键值到 ini 文件。
        /// </summary>
        /// <param name="key">需要写入的键名字符串</param>
        /// <param name="section">需要写入的节名字符串</param>
        /// <param name="value">需要写入的值字符串</param>
        public void Write(string key, string value, string section)
        {
            if (!this.iniExist)
            {
                this.CreateIniFile();
            }
            WritePrivateProfileString(section, key, value ?? "", this.path);
            // 这里对应另一种重载，使用 byte[] 传递。
            /* WritePrivateProfileString(Str2Byte(section), Str2Byte(key), Str2Byte(value ?? ""), path); */
        }

        /// <summary>
        /// 判断 ini 文件键值存在否。
        /// </summary>
        /// <param name="key">需要读取的键名字符串</param>
        /// <param name="section">需要读取的节名字符串</param>
        /// <returns>值为空或空格返回false，否则才是true</returns>
        public bool KeySecExists(string key, string section)
        {
            /* return Read(key, section).Length>0; */
            return !string.IsNullOrWhiteSpace(Read(key, section));
        }

        /// <summary>
        /// 删除 ini 文件，无需指定路径。
        /// </summary>
        public void DeleteFile()
        {
            File.Delete(path);
            this.iniExist = File.Exists(this.path);
        }

        /// <summary>
        /// 将字符串转为 utf8 编码。
        /// </summary>
        /// <param name="s">需要处理的字符串</param>
        /// <returns>储存utf8数字编码的byte数组</returns>
        /* private byte[] Str2Byte(string s)
        {
            return s == null ? null : Encoding.UTF8.GetBytes(s);
        } */

        /// <summary>
        /// 自动判断将 utf8BOM 头去除，变成 utf8。
        /// <para>清除经过 Windows 默认记事本保存后附带上的 BOM。</para>
        /// </summary>
        private void Utf8BOMConvert()
        {
            // 读取文件所有字节，并判断前三位。
            byte[] oldBytes = File.ReadAllBytes(path);
            if (oldBytes != null && oldBytes.Length > 3 &&
                oldBytes[0] == 0xef && oldBytes[1] == 0xbb && oldBytes[2] == 0xbf)
            {
                byte[] newBytes = new byte[oldBytes.Length - 3];
                for (int i = 3; i < oldBytes.Length; i++)
                {
                    newBytes[i - 3] = oldBytes[i];
                }
                File.WriteAllBytes(path, newBytes);
            }
        }

        /// <summary>
        /// 获取 ini 文件的编码，主要确定是否为 utf16LE。
        /// </summary>
        private void GetEncoding()
        {
            byte[] allBytes = File.ReadAllBytes(this.path);
            if (allBytes == null || allBytes.Length < 3)
            {
                this.iniExist = false;
            }
            else if (allBytes[0] == 0xff && allBytes[1] == 0xfe)
            {
                this.iniEncoding = Encoding.Unicode;
            }
        }

        /// <summary>
        /// 创建 utf16le 的 BOM，以便 Win32API 沿用相同编码。
        /// </summary>
        private void CreateIniFile()
        {
            /* using (StreamWriter sw = new StreamWriter(this.path, false, Encoding.Unicode))
            {
                iniExist = File.Exists(path) ? true : false;
            } */
            byte[] uft16lebomBytes = new byte[] {0xff, 0xfe};
            File.WriteAllBytes(this.path, uft16lebomBytes);
            this.iniExist = true;
        }
    }

}
