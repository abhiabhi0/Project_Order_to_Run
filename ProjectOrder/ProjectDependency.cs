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
        private List<string> project_lst = new List<string>();

        public ProjectDependency(string projects, string dependencies)
        {
            Projects = projects;
            Dependencies = dependencies;
        }

        void dfs_helper(string curr_proj, ref Dictionary<string, int> status, ref bool is_cyclic) //using depth first search to find cyclic dependency
        {
            status[curr_proj] = 1;

            if (this.dependencies_dict.ContainsKey(curr_proj))
            {
                foreach (string project in this.dependencies_dict[curr_proj])
                {
                    if (status[project] == 0)
                    {
                        dfs_helper(project, ref status, ref is_cyclic);
                    }
                    else if (status[project] == 1)
                    {
                        is_cyclic = true;
                        return;
                    }
                }
            }
            status[curr_proj] = 2;
        }
        bool check_cyclic_dependency()
        {
            //status = 0 (unprocessed), 1 (processing), 2 (processed)
            Dictionary<string, int> status = new Dictionary<string, int>();
            bool is_cyclic = false;

            for (int i = 0; i < this.project_lst.Count; i++)
            {
                status.Add(this.project_lst[i], 0);
            }

            foreach (string proj in this.project_lst)
            {
                dfs_helper(proj, ref status, ref is_cyclic);

                if (is_cyclic) return is_cyclic;
            }
            
            return is_cyclic;
        }

        private void initialize_indegree()
        {
            for (int i = 0; i < this.project_lst.Count; i++)
            {
                this.indegree.Add(this.project_lst[i], 0);
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
            //List<string> projects_lst = new List<string>();
            this.project_lst = projects_str.Split(',').ToList<string>();

            for (int i = 0; i < project_lst.Count; ++i)
            {
                project_lst[i] = project_lst[i].Trim();
            }

            this.initialize_indegree();
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

            //bool has_non_dependent = false;

            foreach (var ele in indegree)
            {
                if (ele.Value == 0)
                {
                    queue.Enqueue(ele.Key, ele.Value);
                    //has_non_dependent = true;
                }
            }

            bool cyclic_dependency = check_cyclic_dependency();

            if (cyclic_dependency)
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
