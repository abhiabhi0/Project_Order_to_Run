using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOrder
{
    class ProjectDependency
    {
        //String maps to List[strings]
        //Project maps to List of projects dependent on it
        private Dictionary<string, List<string>> dependencies = new Dictionary<string, List<string>>();
        private Dictionary<string, int> indegree =  new Dictionary<string, int>();

        private void initialize_indegree(List<string> projects_lst)
        {
            for (int i = 0; i < projects_lst.Count; i++)
            {
                this.indegree.Add(projects_lst[i], 0);
            }
        }

        private void add_dependencies(string key, string value)
        {
            if (this.dependencies.ContainsKey(key))
            {
                List<string> list = this.dependencies[key];
                if (list.Contains(value) == false)
                {
                    list.Add(value.Remove(0, 1));
                }
            }
            else
            {
                List<string> list = new List<string>();
                list.Add(value.Remove(0, 1));
                this.dependencies.Add(key.Remove(2), list);
            }
        }

        public void find_projects(string projects_str)
        {
            List<string> projects_lst = new List<string>();
            projects_lst = projects_str.Split(',').ToList<string>();

            for (int i = 0; i < projects_lst.Count; ++i)
            {
                projects_lst[i] = projects_lst[i].Trim();
            }
        }
        public void find_dependencies(string dependency_str)
        {
            List<string> dependency_lst = new List<string>();
            dependency_lst = dependency_str.Split(',').ToList<string>();

            for (int i = 0; i < dependency_lst.Count; i++)
            {
                dependency_lst[i] = dependency_lst[i].Trim();

                if (i % 2 != 0)
                {
                    add_dependencies(dependency_lst[i], dependency_lst[i - 1]);
                }
            }
        }

        public void display_dependencies()
        {
            foreach(var ele in dependencies)
            {
                Console.WriteLine(ele.Key + " " + ele.Value[0]);
            }
        }
    }
}
