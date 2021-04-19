USE [BootstrapAdmin]
GO

-- ADMIN/123789
-- User/123789
DELETE From Users where UserName in ('Admin', 'User')
INSERT INTO Users (UserName, Password, PassSalt, DisplayName, RegisterTime, ApprovedTime, ApprovedBy, [Description]) values ('Admin', 'Es7WVgNsJuELwWK8daCqufUBknCsSC0IYDphQZAiGOo=', 'W5vpBEOYRGHkQXatN0t+ECM/U8cHDuEgrq56+zZBk4J481xH', 'Administrator', GetDate(), GetDate(), 'system', N'系統預設創建')
INSERT INTO Users (UserName, Password, PassSalt, DisplayName, RegisterTime, ApprovedTime, ApprovedBy, [Description], App) values ('User', 'tXG/yNffpnm6cThrCH7wf6jN1ic3VHvLoY4OrzKtrZ4=', 'c5cIrRMn8XjB84M/D/X7Lg9uUqQFmYNEdxb/4HWH8OLa4pNZ', N'測試賬號', GetDate(), GetDate(), 'system', N'系統預設創建', 'Demo')

DELETE From Dicts Where Define = 0
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'選單', N'系統選單', N'0', 0)
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'選單', N'外部選單', N'1', 0)
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'應用程式', N'後台管理', N'BA', 0)
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'網站設置', N'網站標題', N'後台管理系統', 0)
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'網站設置', N'網站頁腳', N'2016 © 通用後台管理系統', 0)
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'系統通知', N'用戶注冊', N'0', 0)
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'系統通知', N'程式異常', N'1', 0)
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'系統通知', N'數據庫連接', N'2', 0)
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'通知狀態', N'未處理', N'0', 0)
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'通知狀態', N'已處理', N'1', 0)
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'處理結果', N'同意', N'0', 0)
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'處理結果', N'拒絕', N'1', 0)
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'訊息狀態', N'未讀', N'0', 0)
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'訊息狀態', N'已讀', N'1', 0)
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'訊息標簽', N'一般', N'0', 0)
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'訊息標簽', N'緊要', N'1', 0)
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'頭像地址', N'頭像路徑', N'~/images/uploader/', 0)
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'頭像地址', N'頭像文件', N'default.jpg', 0)
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'網站樣式', N'藍色樣式', N'blue.css', 0)
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'網站樣式', N'黑色樣式', N'black.css', 0)
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'網站樣式', N'AdminLTE', N'lte.css', 0)
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'網站設置', N'使用樣式', N'blue.css', 0)
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'網站設置', N'前台首頁', N'~/Home/Index', 0)

-- 網站UI設置
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'網站設置', N'側邊欄狀態', N'1', 0)
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'網站設置', N'卡片標題狀態', N'1', 0)
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'網站設置', N'固定表頭', N'1', 0)

-- 登錄配置
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'網站設置', N'短信驗證碼登錄', N'1', 0)
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'網站設置', N'OAuth 認證登錄', N'1', 0)

-- 自動鎖屏（秒）預設 30 秒
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'網站設置', N'自動鎖屏時長', N'30', 0)
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'網站設置', N'自動鎖屏', N'0', 0)

-- 是否啟用 Blazor 預設為 0 未啟用
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'網站設置', N'Blazor', N'0', 0)

-- 是否啟用 健康檢查 預設為 0 未啟用 1 啟用
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'網站設置', N'健康檢查', N'1', 0);

-- 時長單位 月
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'網站設置', N'程式異常保留時長', '1', 0)
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'網站設置', N'操作日志保留時長', '12', 0)
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'網站設置', N'登錄日志保留時長', '12', 0)
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'網站設置', N'訪問日志保留時長', '1', 0)

-- 時長單位 天
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'網站設置', N'Cookie保留時長', '7', 0)

-- 地理位置接口
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'網站設置', N'IP地理位置接口', 'None', 0)
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'地理位置服務', N'百度地圖開放平台', 'BaiDuIPSvr', 0);
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'地理位置服務', N'聚合地理位置', 'JuheIPSvr', 0);
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'地理位置服務', N'百度138地理位置', 'BaiDuIP138Svr', 0);

INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'地理位置', N'BaiDuIPSvr', 'http://api.map.baidu.com/location/ip?ak=6lvVPMDlm2gjLpU0aiqPsHXi2OiwGQRj&ip=', 0)
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'地理位置', N'JuheIPSvr', 'http://apis.juhe.cn/ip/ipNew?key=f57102d1b9fadd3f4a1c29072d0c0206&ip=', 0)
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'地理位置', N'BaiDuIP138Svr', 'https://sp0.baidu.com/8aQDcjqpAAV3otqbppnN2DJv/api.php?resource_id=6006&query=', 0)

-- 時長單位 分鐘
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'網站設置', N'IP請求緩存時長', '10', 0)
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'網站設置', N'演示系統', '0', 0)
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'網站設置', N'授權鹽值', 'yjglE2eddCGcS7tTFTDd2DfvqXHgCnMhNhpmx9HJaC9l8GAZ', 0)
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'網站設置', N'哈希結果', '6jTT50HGuk8V+AIsiE4IfqjcER71PBN1DY7gqOLZE7E=', 0)
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'網站設置', N'驗證碼圖床', 'http://imgs.sdgxgz.com/images/', 0)

INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'網站設置', N'預設應用程式', '0', 0)

INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'網站設置', N'後台地址', 'http://localhost:50852', 0)

-- 系統登錄首頁設置
INSERT INTO Dicts (Category, Name, Code, Define) VALUES (N'系統首頁', N'高仿碼雲', N'Login-Gitee', 1);
INSERT INTO Dicts (Category, Name, Code, Define) VALUES (N'系統首頁', N'藍色清新', N'Login-Blue', 1);
INSERT INTO Dicts (Category, Name, Code, Define) VALUES (N'系統首頁', N'系統預設', N'Login', 1);
INSERT INTO Dicts (Category, Name, Code, Define) VALUES (N'系統首頁', N'科技動感', N'Login-Tec', 1);
INSERT INTO Dicts (Category, Name, Code, Define) VALUES (N'系統首頁', N'Admin-LTE', N'Login-LTE', 1);

INSERT INTO Dicts (Category, Name, Code, Define) VALUES (N'網站設置', N'登錄界面', N'Login', 1);

DELETE FROM Navigations Where Category = N'0'
INSERT [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category]) VALUES (0, N'後台管理', 10, N'fa fa-gear', N'~/Admin/Index', N'0')
INSERT [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category]) VALUES (0, N'個人中心', 20, N'fa fa-suitcase', N'~/Admin/Profiles', N'0')
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (@@Identity, N'保存顯示名稱', 10, 'fa fa-fa', 'saveDisplayName', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (@@Identity - 1, N'保存密碼', 20, 'fa fa-fa', 'savePassword', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (@@Identity - 2, N'保存應用', 30, 'fa fa-fa', 'saveApp', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (@@Identity - 3, N'保存樣式', 40, 'fa fa-fa', 'saveTheme', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (@@Identity - 4, N'保存頭像', 50, 'fa fa-fa', 'saveIcon', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (@@Identity - 5, N'保存網站設置', 60, 'fa fa-fa', 'saveUISettings', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category]) VALUES (0, N'系統鎖屏', 25, N'fa fa-television', N'~/Account/Lock', N'0')
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category]) VALUES (0, N'返回前台', 30, N'fa fa-hand-o-left', N'~/Home/Index', N'0')
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category]) VALUES (0, N'網站設置', 40, N'fa fa-fa', N'~/Admin/Settings', N'0')
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (@@Identity, N'保存系統名稱', 10, 'fa fa-fa', 'saveTitle', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (@@Identity - 1, N'保存頁腳設置', 20, 'fa fa-fa', 'saveFooter', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (@@Identity - 2, N'保存樣式', 30, 'fa fa-fa', 'saveTheme', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (@@Identity - 3, N'清理緩存', 40, 'fa fa-fa', 'clearCache', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (@@Identity - 4, N'清理全部緩存', 50, 'fa fa-fa', 'clearAllCache', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (@@Identity - 5, N'登錄設置', 60, 'fa fa-fa', 'loginSettings', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (@@Identity - 6, N'自動鎖屏', 70, 'fa fa-fa', 'lockScreen', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (@@Identity - 7, N'預設應用', 80, 'fa fa-fa', 'defaultApp', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category]) VALUES (0, N'選單管理', 50, N'fa fa-dashboard', N'~/Admin/Menus', N'0')
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (@@Identity, N'新增', 10, 'fa fa-fa', 'add', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (@@Identity - 1, N'編輯', 20, 'fa fa-fa', 'edit', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (@@Identity - 2, N'刪除', 30, 'fa fa-fa', 'del', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (@@Identity - 3, N'分配角色', 40, 'fa fa-fa', 'assignRole', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (0, N'圖示頁面', 50, 'fa fa-fa', '~/Admin/IconView', '0', 1);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (0, N'側邊欄', 55, 'fa fa-fa', '~/Admin/Sidebar', '0', 1);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category]) VALUES (0, N'用戶管理', 60, N'fa fa-user', N'~/Admin/Users', N'0')
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (@@Identity, N'新增', 10, 'fa fa-fa', 'add', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (@@Identity - 1, N'編輯', 20, 'fa fa-fa', 'edit', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (@@Identity - 2, N'刪除', 30, 'fa fa-fa', 'del', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (@@Identity - 3, N'分配部門', 40, 'fa fa-fa', 'assignGroup', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (@@Identity - 4, N'分配角色', 50, 'fa fa-fa', 'assignRole', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category]) VALUES (0, N'角色管理', 70, N'fa fa-sitemap', N'~/Admin/Roles', N'0')
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (@@Identity, N'新增', 10, 'fa fa-fa', 'add', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (@@Identity - 1, N'編輯', 20, 'fa fa-fa', 'edit', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (@@Identity - 2, N'刪除', 30, 'fa fa-fa', 'del', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (@@Identity - 3, N'分配用戶', 40, 'fa fa-fa', 'assignUser', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (@@Identity - 4, N'分配部門', 50, 'fa fa-fa', 'assignGroup', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (@@Identity - 5, N'分配選單', 60, 'fa fa-fa', 'assignMenu', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (@@Identity - 6, N'分配應用', 70, 'fa fa-fa', 'assignApp', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category]) VALUES (0, N'部門管理', 80, N'fa fa-bank', N'~/Admin/Groups', N'0')
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (@@Identity, N'新增', 10, 'fa fa-fa', 'add', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (@@Identity - 1, N'編輯', 20, 'fa fa-fa', 'edit', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (@@Identity - 2, N'刪除', 30, 'fa fa-fa', 'del', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (@@Identity - 3, N'分配用戶', 40, 'fa fa-fa', 'assignUser', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (@@Identity - 4, N'分配角色', 50, 'fa fa-fa', 'assignRole', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category]) VALUES (0, N'字典表維護', 90, N'fa fa-book', N'~/Admin/Dicts', N'0')
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (@@Identity, N'新增', 10, 'fa fa-fa', 'add', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (@@Identity - 1, N'編輯', 20, 'fa fa-fa', 'edit', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (@@Identity - 2, N'刪除', 30, 'fa fa-fa', 'del', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category]) VALUES (0, N'站內訊息', 100, N'fa fa-envelope', N'~/Admin/Messages', N'0')
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category]) VALUES (0, N'任務管理', 110, N'fa fa fa-tasks', N'~/Admin/Tasks', N'0')
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (@@Identity, N'暫停', 10, 'fa fa-fa', 'pause', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (@@Identity - 1, N'日志', 20, 'fa fa-fa', 'info', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category]) VALUES (0, N'通知管理', 120, N'fa fa-bell', N'~/Admin/Notifications', N'0')
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category]) VALUES (0, N'日志管理', 130, N'fa fa-gears', N'#', N'0')
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category]) VALUES (@@Identity, N'操作日志', 10, N'fa fa-edit', N'~/Admin/Logs', N'0')
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category]) VALUES (@@Identity - 1, N'登錄日志', 20, N'fa fa-user-circle-o', N'~/Admin/Logins', N'0')
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category]) VALUES (@@Identity - 2, N'訪問日志', 30, N'fa fa-bars', N'~/Admin/Traces', N'0')
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category]) VALUES (@@Identity - 3, N'SQL日志', 40, N'fa fa-database', N'~/Admin/SQL', N'0')
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category]) VALUES (0, N'在線用戶', 140, N'fa fa-users', N'~/Admin/Online', N'0')
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category]) VALUES (0, N'網站分析', 145, N'fa fa-line-chart', N'~/Admin/Analyse', N'0')
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category]) VALUES (0, N'程式異常', 150, N'fa fa-cubes', N'~/Admin/Exceptions', N'0')
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (@@Identity, N'服務器日志', 10, N'fa fa-fa', N'log', N'0', 2)
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category]) VALUES (0, N'健康檢查', 155, N'fa fa-heartbeat', N'~/Admin/Healths', '0');
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category]) VALUES (0, N'工具集合', 160, N'fa fa-gavel', N'#', N'0')
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category]) VALUES (@@Identity, N'客戶端測試', 10, N'fa fa-wrench', N'~/Admin/Mobile', N'0')
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category]) VALUES (@@Identity - 1, N'API文檔', 10, N'fa fa-wrench', N'~/swagger', N'0')
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category]) VALUES (@@Identity - 2, N'圖示集', 10, N'fa fa-dashboard', N'~/Admin/FAIcon', N'0')

DELETE FROM GROUPS WHERE GroupName = 'Admin'
INSERT [dbo].[Groups] ([GroupCode], [GroupName], [Description]) VALUES ('001', 'Admin', N'系統預設組')

DELETE FROM Roles where RoleName in ('Administrators', 'Default')
INSERT [dbo].[Roles] ([RoleName], [Description]) VALUES (N'Administrators', N'系統管理員')
INSERT [dbo].[Roles] ([RoleName], [Description]) VALUES (N'Default', N'預設用戶，可訪問前台頁面')

-- 角色部門關聯
TRUNCATE Table RoleGroup
INSERT INTO RoleGroup (GroupId, RoleId) SELECT g.Id, r.Id From Groups g left join Roles r on 1=1 where GroupName = 'Admin' and RoleName = N'Administrators'

-- 用戶部門關聯
TRUNCATE Table UserGroup

-- 用戶角色關聯
TRUNCATE Table UserRole
INSERT INTO UserRole (UserId, RoleId) SELECT u.Id, r.Id From Users u left join Roles r on 1=1 where UserName = 'Admin' and RoleName = N'Administrators'
INSERT INTO UserRole (UserId, RoleId) SELECT u.Id, r.Id From Users u left join Roles r on 1=1 where UserName = 'User' and RoleName = N'Default'

-- 角色選單關聯
TRUNCATE Table NavigationRole
INSERT INTO NavigationRole (NavigationID, RoleID) SELECT n.Id, r.Id FROM Navigations n left join Roles r on 1=1 Where RoleName = 'Administrators'
INSERT INTO NavigationRole (NavigationID, RoleID) SELECT n.Id, r.Id FROM Navigations n left join Roles r on 1=1 where RoleName = 'Default' and Name in (N'後台管理', N'個人中心', N'返回前台', N'通知管理')
INSERT INTO NavigationRole (NavigationID, RoleID) SELECT n.Id, r.Id FROM Navigations n left join Roles r on 1=1 where RoleName = 'Default' and ParentId in (select Id from Navigations where Name in (N'個人中心'))

-- Client Data
Declare @AppId nvarchar(50)
set @AppId = N'Demo'
declare @AppName nvarchar(50)
set @AppName = N'測試平台'

Delete From [dbo].[Dicts] Where Category = N'應用程式' and Code = @AppId
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'應用程式', @AppName, @AppId, 0)
Delete From [Dicts] Where Category = '應用首頁' and Name = @AppId
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'應用首頁', @AppId, 'http://localhost:49185', 0)

Delete From [dbo].[Dicts] Where Category = @AppName
Insert Dicts (Category, Name, Code, Define) values (@AppName, N'網站標題', N'前台演示程式', 1);
Insert Dicts (Category, Name, Code, Define) values (@AppName, N'網站頁腳', N'前台演示程式後台權限管理框架', 1);
Insert Dicts (Category, Name, Code, Define) values (@AppName, N'個人中心地址', N'/Admin/Profiles', 1);
Insert Dicts (Category, Name, Code, Define) values (@AppName, N'系統設置地址', N'/Admin/Index', 1);
Insert Dicts (Category, Name, Code, Define) values (@AppName, N'系統通知地址', N'/Admin/Notifications', 1);
INSERT Dicts (Category, Name, Code, Define) VALUES (@AppName, N'favicon', N'/favicon.ico', 1);
INSERT Dicts (Category, Name, Code, Define) VALUES (@AppName, N'網站圖示', '/favicon.png', 1);

-- 選單
DELETE FROM Navigations Where [Application] = @AppId
INSERT [dbo].[Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], [Application]) VALUES (0, N'首頁', 10, N'fa fa-fa', N'~/Home/Index', N'1', @AppId)
INSERT [dbo].[Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], [Application]) VALUES (0, N'測試頁面', 10, N'fa fa-fa', N'~/Home/Index', N'1', @AppId)
INSERT [dbo].[Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], [Application]) VALUES (@@Identity, N'關於', 10, N'fa fa-fa', N'~/Home/Index', N'1', @AppId)

INSERT into [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], [Application]) VALUES (0, N'返回碼雲', 20, 'fa fa-fa', 'https://gitee.com/dotnetchina/BootstrapAdmin', '1', @AppId)

-- 選單授權
INSERT INTO NavigationRole SELECT n.ID, r.ID FROM Navigations n left join Roles r on 1=1 Where r.RoleName = 'Default' and [Application] = @AppId

-- 角色對應用授權
DELETE From RoleApp where AppId = @AppId;
INSERT INTO RoleApp (AppId, RoleId) SELECT @AppId, ID From Roles Where RoleName = 'Default'
INSERT INTO RoleApp (AppId, RoleId) SELECT 'BA', ID From Roles Where RoleName = 'Default'
