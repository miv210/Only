using System;
using System.Collections.Generic;

namespace Only_test.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Position { get; set; } = null!;
        public string Department { get; set; } = null!;
        public string? ParentDepartment { get; set; }
    }
}
