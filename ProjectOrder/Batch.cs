using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOrder
{
    class Batch
    {
        private string Projects { get; set; }
        private string Dependencies { get; set; }
        private List<string> Projects_Order { get; set; }

        ProjectDependency projectDependency = new ProjectDependency();

        public Batch(string projects, string dependencies)        
        {
            Projects = projects;
            Dependencies = dependencies;
        }

        public void find_project_order()
        {
            if (this.Projects.Length == 0 || this.Dependencies.Length == 0)
            {
                throw new InvalidInputFormatException("Invalid Input Format");
            }
            this.Projects_Order = projectDependency.sort_projects(this.Projects, this.Dependencies);
        }

        public void display_projects()
        {
            Console.WriteLine(this.Projects);
        }
        public void display_dependencies()
        {
            Console.WriteLine(this.Dependencies);
        }
        public void display_project_order()
        {
            foreach (string project in this.Projects_Order)
            {
                Console.Write(project + " ");
            }
        }
    }
}
