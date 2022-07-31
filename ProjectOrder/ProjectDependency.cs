using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOrder
{
    class ProjectDependency
    {
        private string Projects { get; set; }
        private string Dependencies { get; set; }

        //String maps to List[strings]
        //Project maps to List of projects dependent on it
        private Dictionary<string, List<string>> dependencies_dict = new Dictionary<string, List<string>>();
        private Dictionary<string, int> indegree =  new Dictionary<string, int>();

        public ProjectDependency(string projects, string dependencies)
        {
            Projects = projects;
            Dependencies = dependencies;
        }

        private void initialize_indegree(List<string> projects_lst)
        {
            for (int i = 0; i < projects_lst.Count; i++)
            {
                this.indegree.Add(projects_lst[i], 0);
            }
        }

        private void update_indegree(string project)
        {
            this.indegree[project] += 1;
        }

        private void add_dependencies(string key, string value)
        {
            //update indegree dictionary
            this.update_indegree(value.Remove(0, 1));
            value = value.Remove(0, 1);
            key = key.Remove(2);
            if (this.dependencies_dict.ContainsKey(key) == true)
            {
                List<string> list = this.dependencies_dict[key];
                if (list.Contains(value) == false)
                {
                    list.Add(value);
                }
            }
            else
            {
                List<string> list = new List<string>();
                list.Add(value);
                this.dependencies_dict.Add(key, list);
            }
        }

        private void find_projects()
        {
            string projects_str = this.Projects;
            List<string> projects_lst = new List<string>();
            projects_lst = projects_str.Split(',').ToList<string>();

            for (int i = 0; i < projects_lst.Count; ++i)
            {
                projects_lst[i] = projects_lst[i].Trim();
            }

            this.initialize_indegree(projects_lst);
        }
        private void find_dependencies()
        {
            string dependency_str = this.Dependencies;
            List<string> dependency_lst = new List<string>();
            dependency_lst = dependency_str.Split(',').ToList<string>();

            for (int i = 0; i < dependency_lst.Count; i++)
            {
                dependency_lst[i] = dependency_lst[i].Trim();

                if (i % 2 != 0)
                {
                    this.add_dependencies(dependency_lst[i], dependency_lst[i - 1]);
                }
            }
        }

        public List<string> sort_projects()
        {
            this.find_projects();
            this.find_dependencies();
            PriorityQueue<string, int> queue = new PriorityQueue<string, int>();
            List<string> project_order = new List<string>();

            bool has_non_dependent = false;

            foreach (var ele in indegree)
            {
                if (ele.Value == 0)
                {
                    queue.Enqueue(ele.Key, ele.Value);
                    has_non_dependent = true;
                }
            }

            //Can't resolve dependency bcoz no Project has indegree equal to 0
            if (!has_non_dependent)
            {
                string msg = "Dependencies for these project cannot be resolved";
                throw new NotResolvedException(msg);
            }

            while (queue.Count > 0)
            {
                string curr_proj = queue.Dequeue();

                if (this.indegree[curr_proj] == 0)
                {
                    project_order.Add(curr_proj);
                }

                if (this.dependencies_dict.ContainsKey(curr_proj))
                {
                    foreach (string dependent_proj in this.dependencies_dict[curr_proj])
                    {
                        if (this.indegree[dependent_proj] > 0)
                        {
                            this.indegree[dependent_proj]--;

                            if (this.indegree[dependent_proj] == 0)
                            {
                                queue.Enqueue(dependent_proj, 0);
                            }
                        }
                    }
                }
            }
            return project_order;
        }
    }
}
