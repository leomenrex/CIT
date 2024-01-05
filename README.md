<img src="/images/Logo.png" align="left"><br><br>

# CIT

> **C**ommand line **I**n **T**ray

运行命令行程序并最小化到托盘区，使用托盘菜单在多个配置间快速切换、重启，并实现日志可自动导出。



<br>

## 设计意图

> 情况一：假如有一个无 GUI 的程序，需要长时间地运行，如果用系统的命令行工具，将一直占据任务栏的位置；

> 情况二：假如这个无 GUI 的程序，需要频繁地切换命令参数、或者是切换配置文件来运行，如果用系统的命令行工具只能每次输入一遍命令，或者重开多个实例手动启停；

CIT 的设计就是为了满足 **单一的程序，长时间运行，多个参数配置切换** 这样的需求。

> [!IMPORTANT]
> 目前 CIT 不支持多程序切换和多实例运行。

<br>

## 屏幕截图
+ 主界面（亮/暗）<br>
  ![主界面亮](/images/mainformlight.png)
  ![主界面暗](/images/mainformdark.png)
  <br><br>
+ 托盘菜单<br>
  ![托盘菜单](/images/traymenu.png)
  <br><br>
+ 日志右键菜单<br>
  ![日志右键菜单](/images/logcontextmenu.png)
  <br><br>
+ 日志菜单<br>
  ![日志按钮菜单](/images/logmenu.png)
  <br><br>
+ 配置明细窗口<br>
  ![配置明细窗口](/images/settingsform.png)
  <br><br>
+ 日志设置窗口<br>
  ![日志设置窗口](/images/logsettingsform.png)
<br>
<br>

## 初次使用
### 安装
在 Release 页面获取的 CIT 是单个的可执行程序，无需安装。只需要：
+ 将 CIT 置于需要运行程序的相同目录；
+ 如果要用到配置文件，也放在相同目录（或者子目录下）；

之后即可运行。

> [!IMPORTANT]
> CIT 只支持选择当前目录（及子目录）下的程序和文件。

<br>

### 填写配置明细

这里举四例，假如有以下命令：

```powershell
frpc.exe                                #配置一
frpc.exe http -i "127.0.0.1" -l 50000   #配置二
frpc.exe --config "./配置文件三.ini"      #配置三
frpc.exe --config "./目录/配置文件四.ini"  #配置四
```
配置明细如图示：

![配置范例](/images/configs.png)

确定保存后 CIT 就会自动开始运行刚才的配置，多个配置下默认执行第一行的配置。

<br>

### 切换配置
![切换配置](/images/configswmenu.png)
在主界面可切换配置，切换后 CIT 会记录该项为默认启动配置。

至此完成初次使用的配置。

<br>

### 后续使用
如果之前录入的配置信息没有变动，对应的文件也没有移动位置，运行 CIT 即可正常使用。
> [!TIP]
> CIT 最小化时会自动隐藏窗口，仅保留托盘区图标；单击图标即可恢复窗口。
> 关闭 CIT 窗口表示退出 CIT。

<br>

## 界面功能
### 配置相关

| 界面 | 说明 |
| :---: | --- |
| ![切换配置按钮](/CIT/Resources/configswitch_light_40.png) | 切换配置按钮 |
| ![修改配置按钮](/CIT/Resources/settings_light_40.png) | 打开配置明细窗口 |
| ![重载配置按钮](/CIT/Resources/reload_light_40.png) | 重新载入配置，方便修改配置后生效 |
| ![托盘菜单](/images/traymenu.png) | 托盘菜单，方便直接通过托盘图标操作配置 |

<br>

### 日志相关

| 界面 | 说明 |
| :---: | --- |
| ![手动导出日志按钮](/CIT/Resources/logoutput_light_40.png) | 手动导出日志，点击后按弹窗提示操作 |
| ![日志按钮菜单](/images/logmenu.png) | 日志导出按钮的下拉菜单 |
| ![日志右键菜单](/images/logcontextmenu.png) | 日志区域选择文本后的右键菜单 |

> [!TIP]
> CIT 默认没有开启日志自动导出和保留，可在“日志设置”中进行设置。

<br>

### 其余杂项

| 界面 | 说明 |
| :---: | --- |
| ![主题切换按钮](/CIT/Resources/themes_light_40.png) | 切换主题按钮，目前 CIT 支持亮、暗两种主题 |
| ![状态显示](/images/statusbar.png) | 主界面状态栏，显示执行程序的运行状态和配置；<br>若执行程序退出，会闪烁提示 |
| ![气球提示](/images/balloontips.png) | 若执行程序退出，会调用系统的提示气球提示（这里为 win10 样式的提示）；<br>托盘图标同样会闪烁 |

<br>

## 配置文件说明
CIT 正常运行时，相同目录下会出现一个 `CIT_settings.ini` 的配置文件。

CIT 从这个文件读取配置，支持手动直接修改，但需注意内部格式和文件编码。

### 内部格式
配置文件分为多个节，`[main]` 节和多个自定义配置名命名的明细节；`[main]` 节储存基本信息和各个明细节的名字；
+ `[main]` 节的键：

| 键名 | 说明 |
| :--- | :--- |
| programname | 执行程序文件绝对路径 |
| keepdata | 重载（非重启）时是否保留日志，`True`/`False` |
| autosave | 重载（非重启）时是否自动导出日志，`True`/`False` |
| logmaxline | 日志最大保留行数，在以下数组中选择一个数：<br> `1000, 3000, 5000, 10000, 30000, 50000, 90000` |
| theme | 主题，`Light`/`Dark` |
| name | 自定义配置名，多个用 `,` 隔开，和明细节节名对应 |

> [!CAUTION]
> `name` 的值一定要和明细节的节名对应，否则整个文件无效。

+ 明细节的键：

| 键名 | 说明 |
| :--- | :--- |
| enabled | 使用当前配置？`True`/`False` |
| argument | 对应命令参数列 |
| configfile | 对应配置文件列 |

<br>

### 文件编码
配置文件默认编码为 UTF-16 Little Endian，CIT 也支持读取 Windows 中文默认使用的 GB 系列编码（GBK、GB2312、GB18030）。

> 其实主要是受限于 Win32API 里的函数 WritePrivateProfileString/GetPrivateProfileString

> [!TIP]
> Windows 默认记事本程序的编码选项：`Unicode`/`UTF-16 LE` 对应的是 UTF-16 Little Endian，`ANSI` 对应的是 GB 系列编码。

<br>

## 构建与兼容性
### 开发环境
+ Visual Studio 2010
+ .Net Framework 4.0

### 通过的测试平台
+ Window 7 SP1 x64
+ Windows 10 LTSC 2015
+ Windows 10 22H2

### 杂项
+ 项目内的 `*.designer.cs` 均经过手动编辑，不保证使用 Visual Studio 窗体设计器可以正常预览。
    + 同时因为使用了自定义颜色类，设计器无法预览实际的颜色。
+ 项目内的 `app.manifest` 为 CIT 的 DPI 感知提供支持，但默认编译出来的可执行文件是没有将此清单合并进去的（单独存放在输出目录；
    + 所以如果要得到已合并清单的可执行文件，要在输出后手动合并；我这里使用`mt.exe -manifest <MANIFESTFILE> -outputresource:<CIT.exe>`

<br>

## 协议
CIT 遵循 BSD 3-Clause Clear License
