using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Models;

namespace ContosoUniversity.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly ContosoUniversity.Data.SchoolContext _context;

        public IndexModel(ContosoUniversity.Data.SchoolContext context)
        {
            _context = context;
        }

        public String NameSort { get; set; }
        public String DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public String CurrentSort { get; set; }

        public PaginatedList<Student> Students { get; set; }

        public async Task<IActionResult> OnGetAsync(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageIndex
        ) 
        {
            // For next sorting request
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";

            CurrentSort = sortOrder;

            if (!String.IsNullOrEmpty(searchString))
            {
                pageIndex = 1;  
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;

            IQueryable<Student> studentsIQ = from s in _context.Students
                                             select s;

            if (!String.IsNullOrEmpty(CurrentFilter))
            {
                studentsIQ = studentsIQ.Where(s => s.LastName.Contains(CurrentFilter) ||
                                s.FirstMidName.Contains(CurrentFilter));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    studentsIQ = studentsIQ.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    studentsIQ = studentsIQ.OrderBy(s => s.EnrollmentDate);
                    break;
                case "date_desc":
                    studentsIQ = studentsIQ.OrderByDescending(s => s.EnrollmentDate);
                    break;
                default:
                    studentsIQ = studentsIQ.OrderBy(s => s.LastName);
                    break;
            }

            int pageSize = 3;
            Students = await PaginatedList<Student>.CreateAsync(studentsIQ, pageIndex ?? 1, pageSize);
            return Page();
        }
    }
}
