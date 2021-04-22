## 提交模板配置

### Windows Fork 

Windows 版本的 Fork 提供了提交模板 （commit message template）功能，配置步骤如下

#### git 配置文件

1. 拷贝仓庫 `scripts\git\commit_msg_template.txt` 文件到當前用户根目录下 `C:\Users\[用户名]\.commit_msg_template.txt`
2. 配置 git 全局配置文件 `C:\Users\[用户名]\.gitconfig` (此文件為隐藏文件)
3. 更新 commit 配置項

```log
[commit]
    template = /Users/argo/.commit_msg_template.txt
```

注意原始文件不是 . 開頭拷贝到跟目录下為 . 開頭文件名（點号開頭文件預設為隐藏文件）

#### Fork 配置步骤

1. 打開要配置的仓庫
2. 點擊菜單欄第二個 **仓庫** 菜單（Repository）
3. 下拉菜單中選中最后一個菜單項 **仓庫設置** 子菜單（Settings for this repository）

如下圖所示  
![輸入圖片說明](https://images.gitee.com/uploads/images/2020/0327/123310_1b9b4af3_554725.png "Screen Shot 2020-03-27 at 12.30.38.png")

4. 切換到 **提交模板** 面板（Commit Template）
5. 勾選使用 **全局配置文件** （Use global git configuration file）

下面的文本框内即出现提交模板内容
