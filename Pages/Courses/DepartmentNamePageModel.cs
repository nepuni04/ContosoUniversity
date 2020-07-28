using ContosoUniversity.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Pages.Courses
{
    public class DepartmentNamePageModel : PageModel
    {
        public SelectList DepartmentNameSL { get; set; }

        public void PopulateDepartmentsDropdownList(
            SchoolContext context, 
            object selectedDepartment = null
        )
        {
            var departmentQuery = from d in context.Departments
                                  orderby d.Name
                                  select d;

            DepartmentNameSL = new SelectList(departmentQuery.AsNoTracking(), 
                "DepartmentID", 
                "Name", 
                selectedDepartment);
        }
    }
}
