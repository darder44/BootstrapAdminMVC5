# BootstrapAdmin

<a href="README.md">English</a> | <span>中文</span>

---

##### Version & Coverage
[![Release](https://img.shields.io/endpoint.svg?logo=Groupon&logoColor=red&color=green&label=release&url=https://admin.blazor.zone/api/Gitee/Releases?userName=dotnetchina)](https://gitee.com/dotnetchina/BootstrapAdmin/releases)
[![Coveralls](https://img.shields.io/coveralls/github/ArgoZhang/BootstrapAdmin/master.svg?logo=ReverbNation&logoColor=green&label=coveralls)](https://coveralls.io/github/ArgoZhang/BootstrapAdmin?branch=master)
[![Codecov](https://img.shields.io/codecov/c/gh/argozhang/bootstrapadmin/master.svg?logo=codecov&label=codecov)](https://codecov.io/gh/argozhang/bootstrapadmin/branch/master)

##### Gitee
[![Appveyor build](https://img.shields.io/endpoint.svg?logo=appveyor&label=build&color=blueviolet&url=https://admin.blazor.zone/api/Gitee/Builds?projName=bootstrapadmin-9m1jm)](https://ci.appveyor.com/project/ArgoZhang/bootstrapadmin-9m1jm)
[![Build Status](https://img.shields.io/appveyor/ci/ArgoZhang/bootstrapadmin-9m1jm/master.svg?logo=appveyor&label=master)](https://ci.appveyor.com/project/ArgoZhang/bootstrapadmin-9m1jm/branch/master)
[![Test](https://img.shields.io/appveyor/tests/ArgoZhang/bootstrapadmin-9m1jm/master.svg?logo=appveyor&)](https://ci.appveyor.com/project/ArgoZhang/bootstrapadmin-9m1jm/branch/master/tests)
[![Issue Status](https://img.shields.io/endpoint.svg?logo=Groupon&logoColor=critical&label=issues&url=https://admin.blazor.zone/api/Gitee/Issues?userName=dotnetchina)](https://gitee.com/dotnetchina/BootstrapAdmin/issues)
[![Pull Status](https://img.shields.io/endpoint.svg?logo=Groupon&logoColor=green&color=success&label=pulls&url=https://admin.blazor.zone/api/Gitee/Pulls?userName=dotnetchina)](https://gitee.com/dotnetchina/BootstrapAdmin/pulls)

##### GitHub
[![Appveyor build](https://img.shields.io/endpoint.svg?logo=appveyor&label=build&color=blueviolet&url=https://admin.blazor.zone/api/Gitee/Builds?projName=bootstrapadmin)](https://ci.appveyor.com/project/ArgoZhang/bootstrapadmin)
[![master status](https://img.shields.io/appveyor/ci/ArgoZhang/bootstrapadmin/master.svg?logo=appveyor&label=master)](https://ci.appveyor.com/project/ArgoZhang/bootstrapadmin/branch/master)
[![Test](https://img.shields.io/appveyor/tests/argozhang/bootstrapadmin/master.svg?logo=appveyor&)](https://ci.appveyor.com/project/ArgoZhang/bootstrapadmin/branch/master/tests)
[![Github build](https://img.shields.io/github/workflow/status/ArgoZhang/BootstrapAdmin/Auto%20Build%20CI/master?label=master&logoColor=green&logo=github)](https://github.com/ArgoZhang/BootstrapAdmin/actions?query=workflow%3A%22Auto+Build+CI%22+branch%3Amaster)
[![Repo Size](https://img.shields.io/github/repo-size/ArgoZhang/BootstrapAdmin.svg?logo=github&logoColor=green&label=repo)](https://github.com/ArgoZhang/BootstrapAdmin)
[![Commit Date](https://img.shields.io/github/last-commit/ArgoZhang/BootstrapAdmin/master.svg?logo=github&logoColor=green&label=commit)](https://github.com/ArgoZhang/BootstrapAdmin)

## 項目介紹
一直需要一款後台管理系統，但是網上很多開源項目都是 **Java** 開发的，本人是 **NET** 平台的對 **Java** 一竅不通，C#版本的本來就少而且還沒有合適的。於是決定自己開发一套後台管理系統。由於前台采用 **Bootstrap** 布局樣式，所以就叫做 **BootstrapAdmin** 。本系統可以用於所有的 Web 應用程序，目前版本已經升級到 **NET CORE** 具備跨平台能力。數據庫方面同時支持多種數據庫，詳細列表見後面**數據庫**的詳細列表，切換數據源僅需更改配置文件無需重啟應用程序，配置簡單靈活。UI 前端使用流行的 Bootstrap 框架布局對移動設備的兼容性非常好，自適應目前市場幾乎所有終端設備。本系統還具備單一後台支持多前台的特色，提供 **單點登錄（SSO）** 的能力。  

使用 NET Core + Bootstrap + PetaPoco + HTML 5 + jQuery 構建的後台管理平台  

### 特別說明
**BootstrapAdmin** 無需二次開发，要做的僅僅是與前台系統集成，前台系統模板工程為 **Bootstrap.Client**   
項目原始出发點是把權限系統從業務系統中剝離出來，項目開发專注於功能，詳細配置說明請點擊 [查看文檔](https://gitee.com/dotnetchina/BootstrapAdmin/wikis/%E7%B3%BB%E7%BB%9F%E9%9B%86%E6%88%90)

### 主要功能  
1. 通過配置與前台網站集成
2. 構建前台系統分層級選單
3. 提供單一後台支持多前台應用配置
4. 提供單點登錄
5. 集成系統認證授權模塊
6. 提供角色，部門，用戶，選單，前台應用程序授權  
角色對用戶授權  
角色對選單授權  
角色對部門授權  
角色對應用程序授權（多個前台應用公用一個後台權限管理系統）  
部門對用戶授權  
7. 提供字典表用於前台網站個性化配置  
8. 完全響應式布局（支持電腦、平板、手機等所有主流設備）
9. 內置多數據源支持，配置簡單立即生效無需重啟
10. 內置數據內存緩存機制，頁面快速響應
11. 內置數據 **操作日誌** 與用戶 **登錄日誌**   
跟蹤記錄用戶 **登錄主機地點**  **瀏覽器**  **操作系統** 信息  

更新日誌：[傳送門](https://gitee.com/dotnetchina/BootstrapAdmin/wikis/更新日誌)

### 優勢
1. 前台系統不用編寫登錄、授權、認證模塊；只負責編寫業務模塊即可
2. 後台系統無需任何二次開发，直接发布即可使用
3. 前台與後台系統分離，分別為不同的系統（域名可獨立）
4. 可擴展為多租戶應用

詳細資料請點擊 [查看文檔](https://gitee.com/dotnetchina/BootstrapAdmin/wikis/%E9%A1%B9%E7%9B%AE%E4%BB%8B%E7%BB%8D)  

### 數據庫
數據庫支持列表如下：  
**MSSQL/Oracle/SQLite/MySql/MariaDB/Postgresql/Firebird/MongoDB**  

### 瀏覽器支持

![chrome](https://img.shields.io/badge/chrome->%3D4.5-success.svg?logo=google%20chrome&logoColor=red)
![firefox](https://img.shields.io/badge/firefox->38-success.svg?logo=mozilla%20firefox&logoColor=red)
![edge](https://img.shields.io/badge/edge->%3D12-success.svg?logo=microsoft%20edge&logoColor=blue)
![ie](https://img.shields.io/badge/ie->%3D11-success.svg?logo=internet%20explorer&logoColor=blue)
![Safari](https://img.shields.io/badge/safari->%3D9-success.svg?logo=safari&logoColor=blue)
![Andriod](https://img.shields.io/badge/andriod->%3D4.4-success.svg?logo=android)
![oper](https://img.shields.io/badge/opera->%3D3.0-success.svg?logo=opera&logoColor=red)

```json
"browserslist": [
  "Chrome >= 45",
  "Firefox >= 38",
  "Edge >= 12",
  "Explorer >= 11",
  "iOS >= 9",
  "Safari >= 9",
  "Android >= 4.4",
  "Opera >= 30"
]
```  

### 移動端支持  
![ios](https://img.shields.io/badge/ios-supported-success.svg?logo=apple&logoColor=white)
![Andriod](https://img.shields.io/badge/andriod-suported-success.svg?logo=android)
![windows](https://img.shields.io/badge/windows-suported-success.svg?logo=windows&logoColor=blue)

|                        |  **Chrome**  |  **Firefox**  |  **Safari**  |  **Android Browser & WebView**  |  **Microsoft Edge**  |
| -------                | ---------    | ---------     | ------       | -------------------------       | --------------       |
|  **iOS**               | Supported    | Supported     | Supported    | N/A                             | Supported            |
|  **Android**           | Supported    | Supported     | N/A          | Android v5.0+ supported         | Supported            |
|  **Windows 10 Mobile** | N/A          | N/A           | N/A          | N/A                             | Supported            |

### 桌面瀏覽器支持  
![macOS](https://img.shields.io/badge/macOS-supported-success.svg?logo=apple&logoColor=white)
![linux](https://img.shields.io/badge/linux-suported-success.svg?logo=linux&logoColor=white)
![windows](https://img.shields.io/badge/windows-suported-success.svg?logo=windows)

|         | Chrome    | Firefox   | Internet Explorer | Microsoft Edge | Opera     | Safari        |
| ------- | --------- | --------- | ----------------- | -------------- | --------- | ------------- |
| Mac     | Supported | Supported | N/A               | N/A            | Supported | Supported     |
| Linux   | Supported | Supported | N/A               | N/A            | N/A       | N/A           |
| Windows | Supported | Supported | Supported, IE10+  | Supported      | Supported | Not supported |

## QQ交流群
[![QQ](https://img.shields.io/badge/QQ-795206915-green.svg?logo=tencent%20qq&logoColor=red)](https://shang.qq.com/wpa/qunwpa?idkey=d381355e50ff91db410c3da3eadb081ba859f64c2877e86343f4709b171f28b8)

## 開发環境搭建
1. 安裝 .net core sdk [官方網址](http://www.microsoft.com/net/download)
2. 安裝 Visual Studio 2019 最新版 [官方網址](https://visualstudio.microsoft.com/vs/getting-started/)
3. 獲取本項目代碼 [BootstrapAdmin](https://gitee.com/dotnetchina/BootstrapAdmin)  

環境搭建教程 [詳細說明](https://gitee.com/dotnetchina/BootstrapAdmin/wikis/%E5%AE%89%E8%A3%85%E6%95%99%E7%A8%8B?sort_id=1333477)   

### 安裝數據庫

本項目默認使用 SQLite 數據庫，內置數據庫腳本 
1. SQLite
2. SqlServer
3. MySql
4. Oracle
5. MongoDB   

數據庫配置 [詳細說明](https://gitee.com/dotnetchina/BootstrapAdmin/wikis/數據庫連接配置?sort_id=1333482)   

## 分支說明  
分支說明 [詳細說明](https://gitee.com/dotnetchina/BootstrapAdmin/wikis/分支說明)

## 演示地址  
[![website1](https://img.shields.io/badge/linux-http://ba.zylweb.cn-success.svg?logo=buzzfeed&logoColor=green)](http://ba.zylweb.cn)
[![website2](https://img.shields.io/badge/linux-http://admin.blazor.zone-success.svg?logo=buzzfeed&logoColor=green)](http://admin.blazor.zone)  

### 登錄用戶名與密碼  
管理賬號：Admin/123789  
普通賬號：User/123789

## Docker 鏡像
[![Docker](https://img.shields.io/docker/cloud/automated/argozhang/ba.svg?logo=docker&logoColor=success)](https://hub.docker.com/r/argozhang/ba)
[![Docker](https://img.shields.io/docker/cloud/build/argozhang/ba.svg?logo=docker&logoColor=success)](https://hub.docker.com/r/argozhang/ba/builds)
[![Docker](https://img.shields.io/github/workflow/status/ArgoZhang/BootstrapAdmin/Docker%20Image%20CI/master?label=Docker%20Image%20CI&logo=github&logoColor=green)](https://github.com/ArgoZhang/BootstrapAdmin/actions?query=workflow%3A%22Docker+Image+CI%22%3Amaster)

### Docker Hub 
鏡像拉取 [傳送門](https://hub.docker.com/r/argozhang/ba)
```bash
docker pull argozhang/ba
```
### 七牛雲:  
鏡像拉取 [傳送門](https://hub.qiniu.com/store/argozhang/ba) 
```bash
docker pull reg.qiniu.com/argozhang/ba
```

## 配置說明
詳細配置說明請點擊 [查看文檔](https://gitee.com/dotnetchina/BootstrapAdmin/wikis) 查看配置說明小節  

## 常見問題Q&A
請點擊 [查看文檔](https://gitee.com/dotnetchina/BootstrapAdmin/wikis/%E5%B8%B8%E8%A7%81%E9%97%AE%E9%A2%98Q&A) 查看常見問題小節  

## 開源協議
[![Gitee license](https://img.shields.io/github/license/argozhang/bootstrapadmin.svg?logo=git&logoColor=red)](https://gitee.com/dotnetchina/BootstrapAdmin/blob/master/LICENSE)

## GVP 獎杯
![項目獎杯](https://images.gitee.com/uploads/images/2021/0112/112021_9d570be1_554725.png "GVP.png")

## 項目截圖

後台首頁

![後台首頁](https://gitee.com/LongbowEnterprise/Pictures/raw/master/BootstrapAdmin/BA02-01.png "BAHome-01.png")

更多截圖請點擊 [查看文檔](https://gitee.com/dotnetchina/BootstrapAdmin/wikis) 查看項目截圖小節  

## 特別鳴謝

1. <a href="https://gitee.com/571183806" target="_blank">**雲龍**</a> 提供雲服務器搭建在線演示系統
2. <a href="https://gitee.com/Ysmc" target="_blank">**一事冇誠**</a> 對 MongoDB 數據庫提供了詳細測試
3. <a href="https://gitee.com/Axxbis" target="_blank">**愛吃油麥菜**</a> 提供雲服務器與二級域名搭建備份演示系統、測試環境以及圖床
4. <a href="https://gitee.com/kasenhoo" target="_blank">**kasenhoo**</a> 對 CentOS + MySql 環境提供詳細測試
5. <a href="https://gitee.com/finally44177" target="_blank">**finally44177**</a> 提供 AdminLTE UI 樣式 PR 對 MongoDB 數據庫提供了詳細測試

## 參與貢獻

1. Fork 本項目
2. 新建 Feat_xxx 分支
3. 提交代碼
4. 新建 Pull Request

## 相關視頻講解

[視頻教材](https://gitee.com/dotnetchina/BootstrapAdmin/wikis/%E8%AF%BE%E7%A8%8B%E5%88%97%E8%A1%A8?sort_id=1916635#%E8%AF%BE%E7%A8%8B%E5%88%97%E8%A1%A8)

## 捐助

如果這個項目對您有所幫助，請掃下方二維碼打賞一杯咖啡。    

<img src="https://gitee.com/LongbowEnterprise/Pictures/raw/master/WeChat/BarCode@2x.png" width="382px;" />
