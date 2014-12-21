##GitCafeClientDemo

### 开发环境
* .NET Framework4.0
* Prism
* MahApps.Metro
* LigGit2Sharp
* SQLite作为本地数据库


### 项目结构
![gitcafe](http://taskdone.qiniudn.com/gitcafe.png/size)
![gitcafe](http://taskdone.qiniudn.com/gitcafe1.png/size)

* GitCafeClientDemo :入口程序 Shell
* GitCafeModule
  * RepositoryBox
  * ToolBar
  * WorkSpace
* 各模块之间，通过*EventAggregator*进行通讯
  *  ChangeRepositoryEvent
  *  LoadRepositoryDBEvent
  *  AddRepositoryDBEvent
  *  ToolBarClickEvent
  *  WindowActiveEvent
* 项目中使用的第三方类库，均放置在ThridPart下
  * NativeBinaries下的相应DLL拷入执行目录
  * Sqlite下的Dll也拷入执行目录

### 项目运行
   本项目中，git和sqlite类库是用C/C++编写的，编译前请将此2个dll拷入执行目录下