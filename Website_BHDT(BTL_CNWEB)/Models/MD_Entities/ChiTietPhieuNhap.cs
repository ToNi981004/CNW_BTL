namespace Website_BHDT_BTL_CNWEB_.Models.MD_Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietPhieuNhap")]
    public partial class ChiTietPhieuNhap
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string IDPhieuNhap { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string IDSanPham { get; set; }

        public double? DonGiaNhap { get; set; }

        public int? SoLuong { get; set; }

        public virtual PhieuNhap PhieuNhap { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
