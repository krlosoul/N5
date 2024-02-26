namespace N5.Core.Dtos.Permission
{
	public class PermissionCreateDto
	{
        public string? EmployeeForename { get; set; }

        public string? EmployeeSurname { get; set; }

        public int PermissionType { get; set; }

        public DateTime PermissionDate { get; set; }
    }
}

