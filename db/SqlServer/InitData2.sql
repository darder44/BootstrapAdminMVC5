USE [BootstrapAdmin]
GO

-- ADMIN/123789
-- User/123789
DELETE From Users where UserName in ('Admin', 'User')
INSERT INTO Users (UserName, Password, PassSalt, DisplayName, RegisterTime, ApprovedTime, ApprovedBy, [Description]) values ('Admin', 'Es7WVgNsJuELwWK8daCqufUBknCsSC0IYDphQZAiGOo=', 'W5vpBEOYRGHkQXatN0t+ECM/U8cHDuEgrq56+zZBk4J481xH', 'Administrator', GetDate() , GetDate(), 'system', N'系統默認創建')
INSERT INTO Users (UserName, Password, PassSalt, DisplayName, RegisterTime, ApprovedTime, ApprovedBy, [Description], App) values ('User', 'tXG/yNffpnm6cThrCH7wf6jN1ic3VHvLoY4OrzKtrZ4=', 'c5cIrRMn8XjB84M/D/X7Lg9uUqQFmYNEdxb/4HWH8OLa4pNZ', N'測試賬號', GetDate(), GetDate(), 'system', N'系統默認創建', 'Demo')

DELETE From Dicts Where Define = 0
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'菜單', N'系統菜單', N'0', 0)
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'菜單', N'外部菜單', N'1', 0)
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'應用程序', N'後台管理', N'BA', 0)
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'網站設置', N'網站標題', N'後台管理系統', 0)
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'網站設置', N'網站頁腳', N'2016 © 通用後台管理系統', 0)
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'系統通知', N'用戶註冊', N'0', 0)
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'系統通知', N'程序異常', N'1', 0)
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'系統通知', N'數據庫連接', N'2', 0)
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'通知狀態', N'未處理', N'0', 0)
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'通知狀態', N'已處理', N'1', 0)
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'處理結果', N'同意', N'0', 0)
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'處理結果', N'拒絕', N'1', 0)
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'消息狀態', N'未讀', N'0', 0)
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'消息狀態', N'已讀', N'1', 0)
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'消息標籤', N'一般', N'0', 0)
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'消息標籤', N'緊要', N'1', 0)
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'頭像地址', N'頭像路徑', N'~/images/uploader/', 0 )
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

-- 自動鎖屏（秒）默認 30 秒
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'網站設置', N'自動鎖屏時長', N'30', 0)
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'網站設置', N'自動鎖屏', N'0', 0)

-- 是否啟用 Blazor 默認為 0 未啟用
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'網站設置', N'Blazor', N'0', 0)

-- 是否啟用 健康檢查 默認為 0 未啟用 1 啟用
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'網站設置', N'健康檢查', N'1', 0);

-- 時長單位 月
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'網站設置', N'程序異常保留時長', '1', 0)
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'網站設置', N'操作日誌保留時長', '12', 0)
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'網站設置', N'登錄日誌保留時長', '12', 0)
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'網站設置', N'訪問日誌保留時長', '1', 0)

-- 時長單位 天
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'網站設置', N'Cookie保留時長', '7', 0)

-- 地理位置接口
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'網站設置', N'IP地理位置接口', 'None', 0)
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'地理位置服務', N'百度地圖開放平台', 'BaiDuIPSvr', 0);
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'地理位置服務', N'聚合地理位置', 'JuheIPSvr', 0);
INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'地理位置服務', N'百度138地理位置', 'BaiDuIP138Svr', 0);

INSERT [dbo].[Dicts] ([Category], [Name], [Code], [Define]) VALUES (N'地理位置',