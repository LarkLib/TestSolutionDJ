using System;

namespace DJ.LMS.WinForms
{
    public enum EditMode
    {
        Add = 0,
        Edit
    }

    public enum PrintCategory
    {
        Label = 0,
        Signboard
    }

    internal struct CellSize
    {
        public int MaxX;
        public int MaxY;
        public int MinX;
        public int MinY;
    }

    public enum ImportTemplateType
    {
        /// <summary>
        /// 拣选信息导入
        /// </summary>
        ChooseImport,
        /// <summary>
        /// 配盘信息导入
        /// </summary>
        AssemblyImport,
        /// <summary>
        /// 库存信息导入
        /// </summary>
        StorageImport
    }
}