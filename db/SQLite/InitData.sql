-- ADMIN/123789
-- User/123789
DELETE From Users where UserName in ('Admin', 'User');
INSERT INTO Users (UserName, Password, PassSalt, DisplayName, RegisterTime, ApprovedTime, ApprovedBy, [Description]) values ('Admin', 'Es7WVgNsJuELwWK8daCqufUBknCsSC0IYDphQZAiGOo=', 'W5vpBEOYRGHkQXatN0t+ECM/U8cHDuEgrq56+zZBk4J481xH', 'Administrator', datetime(CURRENT_TIMESTAMP, 'localtime'), datetime(CURRENT_TIMESTAMP, 'localtime'), 'system', '系統預設創建');
INSERT INTO Users (UserName, Password, PassSalt, DisplayName, RegisterTime, ApprovedTime, ApprovedBy, [Description], [App]) values ('User', 'tXG/yNffpnm6cThrCH7wf6jN1ic3VHvLoY4OrzKtrZ4=', 'c5cIrRMn8XjB84M/D/X7Lg9uUqQFmYNEdxb/4HWH8OLa4pNZ', '測試账号', datetime(CURRENT_TIMESTAMP, 'localtime'), datetime(CURRENT_TIMESTAMP, 'localtime'), 'system', '系統預設創建', 'Demo');

DELETE From Dicts Where Define = 0;
INSERT INTO [Dicts] ([Category], [Name], [Code], [Define]) VALUES ('選單', '系統選單', '0', 0);
INSERT INTO [Dicts] ([Category], [Name], [Code], [Define]) VALUES ('選單', '外部選單', '1', 0);
INSERT INTO [Dicts] ([Category], [Name], [Code], [Define]) VALUES ('應用程式', '後台管理', 'BA', 0);
INSERT INTO [Dicts] ([Category], [Name], [Code], [Define]) VALUES ('網站設置', '網站標題', '後台管理系統', 0);
INSERT INTO [Dicts] ([Category], [Name], [Code], [Define]) VALUES ('網站設置', '網站頁腳', '2016 © 通用後台管理系統', 0);
INSERT INTO [Dicts] ([Category], [Name], [Code], [Define]) VALUES ('系統通知', '用户注册', '0', 0);
INSERT INTO [Dicts] ([Category], [Name], [Code], [Define]) VALUES ('系統通知', '程式异常', '1', 0);
INSERT INTO [Dicts] ([Category], [Name], [Code], [Define]) VALUES ('系統通知', '資料庫連接', '2', 0);
INSERT INTO [Dicts] ([Category], [Name], [Code], [Define]) VALUES ('通知状態', '未處理', '0', 0);
INSERT INTO [Dicts] ([Category], [Name], [Code], [Define]) VALUES ('通知状態', '已處理', '1', 0);
INSERT INTO [Dicts] ([Category], [Name], [Code], [Define]) VALUES ('處理结果', '同意', '0', 0);
INSERT INTO [Dicts] ([Category], [Name], [Code], [Define]) VALUES ('處理结果', '拒绝', '1', 0);
INSERT INTO [Dicts] ([Category], [Name], [Code], [Define]) VALUES ('訊息状態', '未讀', '0', 0);
INSERT INTO [Dicts] ([Category], [Name], [Code], [Define]) VALUES ('訊息状態', '已讀', '1', 0);
INSERT INTO [Dicts] ([Category], [Name], [Code], [Define]) VALUES ('訊息標簽', '一般', '0', 0);
INSERT INTO [Dicts] ([Category], [Name], [Code], [Define]) VALUES ('訊息標簽', '紧要', '1', 0);
INSERT INTO [Dicts] ([Category], [Name], [Code], [Define]) VALUES ('頭像地址', '頭像路徑', '~/images/uploader/', 0);
INSERT INTO [Dicts] ([Category], [Name], [Code], [Define]) VALUES ('頭像地址', '頭像文件', 'default.jpg', 0);
INSERT INTO [Dicts] ([Category], [Name], [Code], [Define]) VALUES ('網站样式', '蓝色样式', 'blue.css', 0);
INSERT INTO [Dicts] ([Category], [Name], [Code], [Define]) VALUES ('網站样式', '黑色样式', 'black.css', 0);
INSERT INTO [Dicts] ([Category], [Name], [Code], [Define]) VALUES ('網站样式', 'AdminLTE', 'lte.css', 0);
INSERT INTO [Dicts] ([Category], [Name], [Code], [Define]) VALUES ('網站設置', '使用样式', 'blue.css', 0);
INSERT INTO [Dicts] ([Category], [Name], [Code], [Define]) VALUES ('網站設置', '前台首頁', '~/Home/Index', 0);

-- 網站UI設置
INSERT INTO [Dicts] ([Category], [Name], [Code], [Define]) VALUES ('網站設置', '侧边欄状態', '1', 0);
INSERT INTO [Dicts] ([Category], [Name], [Code], [Define]) VALUES ('網站設置', '卡片標題状態', '1', 0);
INSERT INTO [Dicts] ([Category], [Name], [Code], [Define]) VALUES ('網站設置', '固定表頭', '1', 0);

-- 登入配置
INSERT INTO [Dicts] ([Category], [Name], [Code], [Define]) VALUES ('網站設置', '短信驗證碼登入', '1', 0);
INSERT INTO [Dicts] ([Category], [Name], [Code], [Define]) VALUES ('網站設置', 'OAuth 認證登入', '1', 0);

-- 自動鎖屏（秒）預設 30 秒
INSERT INTO [Dicts] ([Category], [Name], [Code], [Define]) VALUES ('網站設置', '自動鎖屏時長', '30', 0);
INSERT INTO [Dicts] ([Category], [Name], [Code], [Define]) VALUES ('網站設置', '自動鎖屏', '0', 0);

-- 是否啟用 Blazor 預設為 0 未啟用
INSERT INTO [Dicts] ([Category], [Name], [Code], [Define]) VALUES ('網站設置', 'Blazor', '0', 0);

-- 是否啟用 健康检查 預設為 0 未啟用 1 啟用
INSERT INTO [Dicts] ([Category], [Name], [Code], [Define]) VALUES ('網站設置', '健康检查', '1', 0);

-- 時長單位 月
INSERT INTO [Dicts] ([Category], [Name], [Code], [Define]) VALUES ('網站設置', '程式异常保留時長', '1', 0);
INSERT INTO [Dicts] ([Category], [Name], [Code], [Define]) VALUES ('網站設置', '操作日誌保留時長', '12', 0);
INSERT INTO [Dicts] ([Category], [Name], [Code], [Define]) VALUES ('網站設置', '登入日誌保留時長', '12', 0);
INSERT INTO [Dicts] ([Category], [Name], [Code], [Define]) VALUES ('網站設置', '訪問日誌保留時長', '1', 0);

-- 時長單位 天
INSERT INTO [Dicts] ([Category], [Name], [Code], [Define]) VALUES ('網站設置', 'Cookie保留時長', '7', 0);

INSERT INTO [Dicts] ([Category], [Name], [Code], [Define]) VALUES ('網站設置', 'IP地理位置接口', 'None', 0);
INSERT INTO [Dicts] ([Category], [Name], [Code], [Define]) VALUES ('地理位置服务', '百度地图开放平台', 'BaiDuIPSvr', 0);
INSERT INTO [Dicts] ([Category], [Name], [Code], [Define]) VALUES ('地理位置服务', '聚合地理位置', 'JuheIPSvr', 0);
INSERT INTO [Dicts] ([Category], [Name], [Code], [Define]) VALUES ('地理位置服务', '百度138地理位置', 'BaiDuIP138Svr', 0);

INSERT INTO [Dicts] ([Category], [Name], [Code], [Define]) VALUES ('地理位置', 'BaiDuIPSvr', 'http://api.map.baidu.com/location/ip?ak=6lvVPMDlm2gjLpU0aiqPsHXi2OiwGQRj&ip=', 0);
INSERT INTO [Dicts] ([Category], [Name], [Code], [Define]) VALUES ('地理位置', 'JuheIPSvr', 'http://apis.juhe.cn/ip/ipNew?key=f57102d1b9fadd3f4a1c29072d0c0206&ip=', 0);
INSERT INTO [Dicts] ([Category], [Name], [Code], [Define]) VALUES ('地理位置', 'BaiDuIP138Svr', 'https://sp0.baidu.com/8aQDcjqpAAV3otqbppnN2DJv/api.php?resource_id=6006&query=', 0);

-- 時長單位 分钟
INSERT INTO [Dicts] ([Category], [Name], [Code], [Define]) VALUES ('網站設置', 'IP請求缓存時長', '10', 0);

-- 演示系統
INSERT INTO [Dicts] ([Category], [Name], [Code], [Define]) VALUES ('網站設置', '演示系統', '0', 0);
-- 授權密碼預設為 123789
INSERT INTO [Dicts] ([Category], [Name], [Code], [Define]) VALUES ('網站設置', '授權盐值', 'yjglE2eddCGcS7tTFTDd2DfvqXHgCnMhNhpmx9HJaC9l8GAZ', 0);
INSERT INTO [Dicts] ([Category], [Name], [Code], [Define]) VALUES ('網站設置', '哈希结果', '6jTT50HGuk8V+AIsiE4IfqjcER71PBN1DY7gqOLZE7E=', 0);

INSERT INTO [Dicts] ([Category], [Name], [Code], [Define]) VALUES ('網站設置', '驗證碼图床', 'http://imgs.sdgxgz.com/images/', 0);

INSERT INTO [Dicts] ([Category], [Name], [Code], [Define]) VALUES ('網站設置', '預設應用程式', '0', 0);

INSERT INTO [Dicts] ([Category], [Name], [Code], [Define]) VALUES ('網站設置', '後台地址', 'http://localhost:50852', 0);

-- 系統登入首頁設置
INSERT INTO [Dicts] ([Category], [Name], [Code], [Define]) VALUES ('系統首頁', '高仿碼云', 'Login-Gitee', 1);
INSERT INTO [Dicts] ([Category], [Name], [Code], [Define]) VALUES ('系統首頁', '蓝色清新', 'Login-Blue', 1);
INSERT INTO [Dicts] ([Category], [Name], [Code], [Define]) VALUES ('系統首頁', '系統預設', 'Login', 1);
INSERT INTO Dicts (Category, Name, Code, Define) VALUES ('系統首頁', '科技動感', 'Login-Tec', 1);
INSERT INTO Dicts (Category, Name, Code, Define) VALUES ('系統首頁', 'Admin-LTE', 'Login-LTE', 1);

INSERT INTO [Dicts] ([Category], [Name], [Code], [Define]) VALUES ('網站設置', '登入界面', 'Login', 1);

DELETE FROM Navigations Where Category = '0';
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category]) VALUES (0, '後台管理', 10, 'fa fa-gear', '~/Admin/Index', '0');
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category]) VALUES (0, '個人中心', 20, 'fa fa-suitcase', '~/Admin/Profiles', '0');
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (last_insert_rowid(), '保存顯示名稱', 10, 'fa fa-fa', 'saveDisplayName', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (last_insert_rowid() - 1, '保存密碼', 20, 'fa fa-fa', 'savePassword', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (last_insert_rowid() - 2, '保存應用', 30, 'fa fa-fa', 'saveApp', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (last_insert_rowid() - 3, '保存样式', 40, 'fa fa-fa', 'saveTheme', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (last_insert_rowid() - 4, '保存頭像', 50, 'fa fa-fa', 'saveIcon', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (last_insert_rowid() - 5, '保存網站設置', 60, 'fa fa-fa', 'saveUISettings', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category]) VALUES (0, '系統鎖屏', 25, 'fa fa-television', '~/Account/Lock', '0');
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category]) VALUES (0, '返回前台', 30, 'fa fa-hand-o-left', '~/Home/Index', '0');
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category]) VALUES (0, '網站設置', 40, 'fa fa-fa', '~/Admin/Settings', '0');
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (last_insert_rowid(), '保存系統名稱', 10, 'fa fa-fa', 'saveTitle', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (last_insert_rowid() - 1, '保存頁腳設置', 20, 'fa fa-fa', 'saveFooter', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (last_insert_rowid() - 2, '保存样式', 30, 'fa fa-fa', 'saveTheme', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (last_insert_rowid() - 3, '清理缓存', 40, 'fa fa-fa', 'clearCache', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (last_insert_rowid() - 4, '清理全部缓存', 50, 'fa fa-fa', 'clearAllCache', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (last_insert_rowid() - 5, '登入設置', 60, 'fa fa-fa', 'loginSettings', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (last_insert_rowid() - 6, '自動鎖屏', 70, 'fa fa-fa', 'lockScreen', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (last_insert_rowid() - 7, '預設應用', 80, 'fa fa-fa', 'defaultApp', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category]) VALUES (0, '選單管理', 50, 'fa fa-dashboard', '~/Admin/Menus', '0');
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (last_insert_rowid(), '新增', 10, 'fa fa-fa', 'add', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (last_insert_rowid() - 1, '編輯', 20, 'fa fa-fa', 'edit', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (last_insert_rowid() - 2, '刪除', 30, 'fa fa-fa', 'del', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (last_insert_rowid() - 3, '分配角色', 40, 'fa fa-fa', 'assignRole', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (0, '图標頁面', 50, 'fa fa-fa', '~/Admin/IconView', '0', 1);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (0, '侧边欄', 55, 'fa fa-fa', '~/Admin/Sidebar', '0', 1);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category]) VALUES (0, '用户管理', 60, 'fa fa-user', '~/Admin/Users', '0');
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (last_insert_rowid(), '新增', 10, 'fa fa-fa', 'add', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (last_insert_rowid() - 1, '編輯', 20, 'fa fa-fa', 'edit', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (last_insert_rowid() - 2, '刪除', 30, 'fa fa-fa', 'del', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (last_insert_rowid() - 3, '分配部门', 40, 'fa fa-fa', 'assignGroup', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (last_insert_rowid() - 4, '分配角色', 50, 'fa fa-fa', 'assignRole', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category]) VALUES (0, '角色管理', 70, 'fa fa-sitemap', '~/Admin/Roles', '0');
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (last_insert_rowid(), '新增', 10, 'fa fa-fa', 'add', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (last_insert_rowid() - 1, '編輯', 20, 'fa fa-fa', 'edit', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (last_insert_rowid() - 2, '刪除', 30, 'fa fa-fa', 'del', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (last_insert_rowid() - 3, '分配用户', 40, 'fa fa-fa', 'assignUser', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (last_insert_rowid() - 4, '分配部门', 50, 'fa fa-fa', 'assignGroup', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (last_insert_rowid() - 5, '分配選單', 60, 'fa fa-fa', 'assignMenu', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (last_insert_rowid() - 6, '分配應用', 70, 'fa fa-fa', 'assignApp', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category]) VALUES (0, '部门管理', 80, 'fa fa-bank', '~/Admin/Groups', '0');
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (last_insert_rowid(), '新增', 10, 'fa fa-fa', 'add', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (last_insert_rowid() - 1, '編輯', 20, 'fa fa-fa', 'edit', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (last_insert_rowid() - 2, '刪除', 30, 'fa fa-fa', 'del', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (last_insert_rowid() - 3, '分配用户', 40, 'fa fa-fa', 'assignUser', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (last_insert_rowid() - 4, '分配角色', 50, 'fa fa-fa', 'assignRole', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category]) VALUES (0, '字典表维护', 90, 'fa fa-book', '~/Admin/Dicts', '0');
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (last_insert_rowid(), '新增', 10, 'fa fa-fa', 'add', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (last_insert_rowid() - 1, '編輯', 20, 'fa fa-fa', 'edit', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (last_insert_rowid() - 2, '刪除', 30, 'fa fa-fa', 'del', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category]) VALUES (0, '站内訊息', 100, 'fa fa-envelope', '~/Admin/Messages', '0');
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category]) VALUES (0, '任務管理', 110, 'fa fa fa-tasks', '~/Admin/Tasks', '0');
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (last_insert_rowid(), '暂停', 10, 'fa fa-fa', 'pause', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (last_insert_rowid() - 1, '日誌', 20, 'fa fa-fa', 'info', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category]) VALUES (0, '通知管理', 120, 'fa fa-bell', '~/Admin/Notifications', '0');
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category]) VALUES (0, '系統日誌', 130, 'fa fa-gears', '#', '0');
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category]) VALUES (last_insert_rowid(), '操作日誌', 10, 'fa fa-edit', '~/Admin/Logs', '0');
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category]) VALUES (last_insert_rowid() - 1, '登入日誌', 20, 'fa fa-user-circle-o', '~/Admin/Logins', '0');
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category]) VALUES (last_insert_rowid() - 2, '訪問日誌', 30, 'fa fa-bars', '~/Admin/Traces', '0');
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category]) VALUES (last_insert_rowid() - 3, 'SQL日誌', 40, 'fa fa-database', '~/Admin/SQL', '0');
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category]) VALUES (0, '線上用户', 140, 'fa fa-users', '~/Admin/Online', '0');
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category]) VALUES (0, '網站分析', 145, 'fa fa-line-chart', '~/Admin/Analyse', '0');
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category]) VALUES (0, '程式异常', 150, 'fa fa-cubes', '~/Admin/Exceptions', '0');
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], IsResource) VALUES (last_insert_rowid(), '服务器日誌', 10, 'fa fa-fa', 'log', '0', 2);
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category]) VALUES (0, '健康检查', 155, 'fa fa-heartbeat', '~/Admin/Healths', '0');
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category]) VALUES (0, '工具集合', 160, 'fa fa-gavel', '#', '0');
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category]) VALUES (last_insert_rowid(), '客户端測試', 10, 'fa fa-wrench', '~/Admin/Mobile', '0');
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category]) VALUES (last_insert_rowid() - 1, 'API文档', 20, 'fa fa-wrench', '~/swagger', '0');
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category]) VALUES (last_insert_rowid() - 2, '图標集', 30, 'fa fa-dashboard', '~/Admin/FAIcon', '0');

-- 控件集合選單
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category]) VALUES (0, '控件集合', 170, 'fa fa-stethoscope', '#', '0');
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category]) VALUES (last_insert_rowid(), '行為式驗證碼', 10, 'fa fa-wrench', 'https://gitee.com/LongbowEnterprise/SliderCaptcha', '0');
INSERT INTO [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category]) VALUES (last_insert_rowid() - 1, '下拉框', 20, 'fa fa-bars', 'http://longbowenterprise.gitee.io/longbow-select/', '0');

DELETE FROM GROUPS WHERE GroupName = 'Admin';
INSERT INTO [Groups] ([GroupCode], [GroupName], [Description]) VALUES ('001', 'Admin', '系統預設組');

DELETE FROM Roles where RoleName in ('Administrators', 'Default');
INSERT INTO [Roles] ([RoleName], [Description]) VALUES ('Administrators', '系統管理员');
INSERT INTO [Roles] ([RoleName], [Description]) VALUES ('Default', '預設用户，可訪問前台頁面');

DELETE FROM RoleGroup;
INSERT INTO RoleGroup (GroupId, RoleId) SELECT g.Id, r.Id From Groups g left join Roles r on 1=1 where GroupName = 'Admin' and RoleName = 'Administrators';

DELETE FROM UserGroup;

DELETE FROM UserRole;
INSERT INTO UserRole (UserId, RoleId) SELECT u.Id, r.Id From Users u left join Roles r on 1=1 where UserName = 'Admin' and RoleName = 'Administrators';
INSERT INTO UserRole (UserId, RoleId) SELECT u.Id, r.Id From Users u left join Roles r on 1=1 where UserName = 'User' and RoleName = 'Default';

DELETE FROM NavigationRole;
INSERT INTO NavigationRole (NavigationID, RoleID) SELECT n.Id, r.Id FROM Navigations n left join Roles r on 1=1 Where RoleName = 'Administrators';
INSERT INTO NavigationRole (NavigationID, RoleID) SELECT n.Id, r.Id FROM Navigations n left join Roles r on 1=1 Where RoleName = 'Default' and Name in ('後台管理', '個人中心', '返回前台', '通知管理');
INSERT INTO NavigationRole (NavigationID, RoleID) SELECT n.Id, r.Id FROM Navigations n left join Roles r on 1=1 Where RoleName = 'Default' and ParentId in (select id from Navigations where Name in ('個人中心'));

-- Client Data
Delete From [Dicts] Where Category = '應用程式' and Code = 'Demo';
INSERT INTO [Dicts] ([Category], [Name], [Code], [Define]) VALUES ('應用程式', '測試平台', 'Demo', 0);
Delete From [Dicts] Where Category = '應用首頁' and Name = 'Demo';
INSERT INTO [Dicts] ([Category], [Name], [Code], [Define]) VALUES ('應用首頁', 'Demo', 'http://localhost:49185', 0);

Delete From [Dicts] Where Category = '測試平台';
Insert into Dicts (Category, [Name], Code, Define) values ('測試平台', '網站標題', '前台演示系統', 1);
Insert into Dicts (Category, [Name], Code, Define) values ('測試平台', '網站頁腳', '前台演示程式後台權限管理框架', 1);
Insert into Dicts (Category, [Name], Code, Define) values ('測試平台', '個人中心地址', '/Admin/Profiles', 1);
Insert into Dicts (Category, [Name], Code, Define) values ('測試平台', '系統設置地址', '/Admin/Index', 1);
Insert into Dicts (Category, [Name], Code, Define) values ('測試平台', '系統通知地址', '/Admin/Notifications', 1);
INSERT INTO Dicts (Category, [Name], Code, Define) VALUES ('測試平台', 'favicon', '/favicon.ico', 1);
INSERT INTO Dicts (Category, [Name], Code, Define) VALUES ('測試平台', '網站图標', '/favicon.png', 1);

Delete from [Navigations] where Application = 'Demo';
INSERT into [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], [Application]) VALUES (0, '首頁', 10, 'fa fa-fa', '~/Home/Index', '1', 'Demo');

INSERT into [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], [Application]) VALUES (0, '測試頁面', 20, 'fa fa-fa', '#', '1', 'Demo');
INSERT into [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], [Application]) VALUES (last_insert_rowid(), '關於', 10, 'fa fa-fa', '~/Home/About', '1', 'Demo');

INSERT into [Navigations] ([ParentId], [Name], [Order], [Icon], [Url], [Category], [Application]) VALUES (0, '返回碼云', 20, 'fa fa-fa', 'https://gitee.com/dotnetchina/BootstrapAdmin', '1', 'Demo');

-- 選單授權
INSERT INTO NavigationRole (NavigationId, RoleId) SELECT n.ID, r.ID FROM Navigations n left join Roles r on 1=1 Where r.RoleName = 'Default' and [Application] = 'Demo';

-- 角色对應用授權
DELETE From RoleApp where AppId in ('Demo', 'BA');
INSERT INTO RoleApp (AppId, RoleId) SELECT 'Demo', ID From Roles Where RoleName = 'Default';
INSERT INTO RoleApp (AppId, RoleId) SELECT 'BA', ID From Roles Where RoleName = 'Default';
