using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace CIT
{
    /// <summary>
    /// 用于储存配置明细结构的类。
    /// </summary>
    internal class Config
    {
        public string CurNickName { get; set; }
        public string ProgramName { get; set; }
        public Dictionary<string, string[]> MainDic { get; set; }
        public int LogMaxLine { get; set; }
        public bool IsKeepData { get; set; }
        public bool IsAutoSave { get; set; }
        public Themes Theme { get; set; }
        
        /// <summary>
        /// 初始化 Config，并赋默认值。
        /// </summary>
        public Config()
        {
            CurNickName = ProgramName = "";
            MainDic = new Dictionary<string, string[]>();
            LogMaxLine = 1000;
            IsAutoSave = IsKeepData = false;
            Theme = Themes.Light;
        }
    }

    /// <summary>
    /// 主题枚举。
    /// </summary>
    internal enum Themes
    {
        Dark,
        Light
    }

    /// <summary>
    /// Config 变量合法性检查类，同时沟通 ini 文件。
    /// </summary>
    internal class ConfigHandler
    {
        private Config c;
        private RWIni ini;
        public bool IniExist { get { return this.ini.IniExist; } }
        
        /// <summary>
        /// 初始化 ConfigHandler。
        /// </summary>
        /// <param name="config">获取实例化的Config</param>
        /// <param name="iniFile">可选获取实例化的RWIni</param>
        public ConfigHandler(Config config, RWIni iniFile = null)
        {
            this.ini = iniFile ?? new RWIni();
            this.c = config;
        }

        /// <summary>
        /// 主窗体初始化时检查，组合了多种检查，主要对接主窗体。
        /// </summary>
        /// <returns>返回错误提示的文本</returns>
        public string FormInitializeCheck()
        {
            string criticalIssue = "";
            string normalIssue = "";
            if ((criticalIssue = this.IniSpecificationCheck()) != "")
            {
                return "ini 配置文件存在以下问题：\r\n" + criticalIssue + "\r\n\r\n需要重新填写设置";
            }
            if ((normalIssue = this.Ini2Var()) != "")
            {
                normalIssue = "ini 配置文件存在以下问题：\r\n" + normalIssue + "\r\n现正常启动";
            }
            if ((criticalIssue = this.VarCheck(this.c.MainDic, this.c.ProgramName)) != "")
            {
                return "明细配置存在以下问题：\r\n" + criticalIssue + "\r\n需要重新填写设置";
            }
            else
            {
                return normalIssue;
            }

        }

        /// <summary>
        /// ini 文件基本格式检查。
        /// </summary>
        /// <returns>返回错误提示的文本，表示需要重新填写配置</returns>
        private string IniSpecificationCheck()
        {
            if (!this.ini.KeySecExists("name", "main") || !this.ini.KeySecExists("programname", "main"))
            {
                return "文件编码不支持或基本节格式不符";
            }
            string[] nicknames = this.ini.Read("name", "main").Split(',');
            foreach (string name in nicknames)
            {
                if (this.c.MainDic.ContainsKey(name))
                {
                    this.c.MainDic.Clear();
                    return "自定义别名存在重复";
                }
                this.c.MainDic.Add(name, null);
            }
            foreach (string name in this.c.MainDic.Keys)
            {
                if (!this.ini.KeySecExists("enabled", name))
                {
                    this.c.MainDic.Clear();
                    return "明细节存在缺失";
                }
            }
            return "";
        }

        /// <summary>
        /// 读取 ini 键值到变量，同时检查不影响启动的变量。
        /// </summary>
        /// <returns>返回错误提示的文本，此时错误项取默认值</returns>
        private string Ini2Var()
        {
            this.c.ProgramName = this.ini.Read("programname", "main");
            var dic = new Dictionary<string, string>();
            string name;
            string issue1 = "";
            for (int i = 0; i < this.c.MainDic.Count; i++)
            // foreach 循环中不能修改集合自身成员。
            {
                KeyValuePair<string, string[]> kv = this.c.MainDic.ElementAt(i);
                name = kv.Key;
                this.c.MainDic[name] = new string[2] { this.ini.Read("argument", name), this.ini.Read("configfile", name) };
                // 检查选定了哪个配置启动。
                if (i == 0)
                {
                    this.c.CurNickName = name;
                }
                if (this.ini.Read("enabled", name) == "True")
                {
                    if (dic.ContainsValue("True"))
                    {
                        issue1 = "重复选定多个启动配置，默认更改为取首个配置，可在主界面自行切换配置\r\n";
                    }
                    else
                    {
                        dic.Add(name, this.ini.Read("enabled", name));
                        this.c.CurNickName = name;
                    }
                }
                else if (i == (this.c.MainDic.Count - 1) & !dic.ContainsValue("True"))
                {
                    issue1 = "当前未选择启动配置，默认更改为取首个配置，可在主界面自行切换配置\r\n";
                }
            }
            // 日志四项、主题逐个检查，通过则取值，否则跳过留默认值。
            string issue2 = "";
            string[] tfrng = { "true", "false" };
            if (tfrng.Contains(this.ini.Read("autosave", "main").ToLower()))
            {
                this.c.IsAutoSave = Convert.ToBoolean(this.ini.Read("autosave", "main"));
            }
            else
            {
                issue2 = "自动导出日志部分设置缺失，已更改为默认值\r\n";
            }
            if (tfrng.Contains(this.ini.Read("keepdata", "main").ToLower()))
            {
                this.c.IsKeepData = Convert.ToBoolean(this.ini.Read("keepdata", "main"));
            }
            else
            {
                issue2 += "保留日志部分设置缺失，已更改为默认值\r\n";
            }
            if (this.c.IsKeepData & this.c.IsAutoSave)
            {
                this.c.IsKeepData = this.c.IsAutoSave = false;
            }
            int[] lnrng = { 1000, 3000, 5000, 10000, 30000, 50000, 90000 };
            int j;
            if (int.TryParse(this.ini.Read("logmaxline", "main"), out j) && lnrng.Contains(j))
            {
                this.c.LogMaxLine = j;
            }
            else
            {
                issue2 += "保留行部分设置缺失，已更改为默认值\r\n";
            }
            if (Enum.IsDefined(typeof(Themes), this.ini.Read("theme", "main")))
            {
                this.c.Theme = (Themes)Enum.Parse(typeof(Themes), this.ini.Read("theme", "main"));
            }
            else
            {
                issue2 += "主题部分配置缺失，已更改为默认值\r\n";
            } 
            string issue3 = "";
            if (this.ini.IniEncoding != Encoding.Unicode)
            {
                issue3 = "已对配置文件重新编码\r\n";
            }
            return issue1 + issue2 + issue3;
        }

        /// <summary>
        /// 检查基础变量合法性。
        /// </summary>
        /// <param name="dic">储存了需要检查数据的字典，结构同MainDic</param>
        /// <param name="programName">执行程序文件的字符串</param>
        /// <returns>返回错误提示文本，表示需要重新填写设置</returns>
        public string VarCheck(Dictionary<string, string[]> dic, string programName)
        {
            string argument, configFile;
            string nickNameIssue = null, argumentIssue = null, cfgIssue = null, programNameIssue = null;
            int count = 0;
            foreach (string name in dic.Keys)
            {
                // 用于返回行号。
                count += 1;
                argument = dic[name][0];
                configFile = dic[name][1];
                if (name.Length > 20)
                {
                    nickNameIssue = "部分行别名过长，超过20字符限制";
                }
                if (configFile != "")
                {
                    if (!File.Exists(configFile) || !configFile.Contains(MainForm.BaseDir))
                    {
                        if (cfgIssue == null)
                        {
                            cfgIssue = count.ToString();
                        }
                        else
                        {
                            cfgIssue = cfgIssue + "," + count.ToString();
                        }
                    }
                    if (argument == "" || (argument != "" & !argument.Contains("%")))
                    {
                        if (argumentIssue == null)
                        {
                            argumentIssue = count.ToString();
                        }
                        else
                        {
                            argumentIssue = argumentIssue + "," + count.ToString();
                        }
                    }
                }
            }
            if (!File.Exists(programName) || !programName.Contains(MainForm.BaseDir))
            {
                programNameIssue = "执行程序文件不存在，或超出目录及子目录范围";
            }
            nickNameIssue = nickNameIssue == null ? "" : "检查到 " + nickNameIssue + "\r\n";
            argumentIssue = argumentIssue == null ? "" : "检查到第 " + argumentIssue + " 行命令缺失%\r\n";
            cfgIssue = cfgIssue == null ? "" : "检查到第 " + cfgIssue + " 行指向的文件不存在，或超出目录及子目录范围\r\n";
            programNameIssue = programNameIssue == null ? "" : programNameIssue + "\r\n";
            return nickNameIssue + argumentIssue + cfgIssue + programNameIssue;
        }

        /// <summary>
        /// 将 Config 变量写入 ini 文件。
        /// </summary>
        public void Var2Ini()
        {
            this.ini.DeleteFile();
            this.ini.Write("programname", this.c.ProgramName, "main");
            this.ini.Write("keepdata", this.c.IsKeepData.ToString(), "main");
            this.ini.Write("autosave", this.c.IsAutoSave.ToString(), "main");
            this.ini.Write("logmaxline", this.c.LogMaxLine.ToString(), "main");
            this.ini.Write("theme", this.c.Theme.ToString(), "main");

            string nicknames = string.Join(",", this.c.MainDic.Keys);
            this.ini.Write("name", nicknames, "main");

            foreach (string name in this.c.MainDic.Keys)
            {
                if (this.c.CurNickName == name)
                {
                    this.ini.Write("enabled", "True", name);
                }
                else
                {
                    this.ini.Write("enabled", "False", name);
                }
                this.ini.Write("argument", this.c.MainDic[name][0], name);
                this.ini.Write("configfile", this.c.MainDic[name][1], name);
            }
        }
    }
}