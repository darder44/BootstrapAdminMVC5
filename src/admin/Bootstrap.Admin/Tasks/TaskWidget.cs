namespace Bootstrap.Admin
{
    /// <summary>
    /// 任務描述類
    /// </summary>
    public class TaskWidget
    {
        /// <summary>
        /// 獲得/設置 任務名稱
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// 獲得/設置 任務執行體名稱
        /// </summary>
        public string TaskExecutorName { get; set; } = "";

        /// <summary>
        /// 獲得/設置 Cron 任務表達式
        /// </summary>
        public string CronExpression { get; set; } = "";
    }
}
