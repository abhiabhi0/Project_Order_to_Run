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
                //Console.WriteLine("adding" + projects_lst[i]);
                this.indegree.Add(projects_lst[i], 0);
            }
        }

        private void update_indegree(string project)
        {
            this.indegree[project] += 1;
            //Console.WriteLine(project +" updated " + this.indegree[project]);
        }

        private void add_dependencies(string key, string value)
        {
            //update indegree dictionary
            this.update_indegree(value.Remove(0, 1));

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

            this.initialize_indegree(projects_lst);
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
                    this.add_dependencies(dependency_lst[i], dependency_lst[i - 1]);
                }
            }
        }

        public List<string> sort_projects()
        {
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

            //Can't resolve dependency bcoz new Project has indegree equal to 0
            if (!has_non_dependent)
            {
                string msg = "Dependencies for these project cannot be resolved";
                throw new NotResolvedException(msg);
            }

            while (queue.Count > 0)
            {
                string curr_proj = queue.Dequeue();
                //Console.WriteLine("curr proj" + curr_proj);

                if (this.indegree[curr_proj] == 0)
                {
                    //Console.WriteLine("curr proj indegree value" + this.indegree[curr_proj]);
                    project_order.Add(curr_proj);
                }

                if (this.dependencies.ContainsKey(curr_proj))
                {
                    foreach (string dependent_proj in this.dependencies[curr_proj])
                    {
                        //Console.WriteLine("depedent proj " + dependent_proj);
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

        public void display_dependencies()
        {
            foreach(var ele in dependencies)
            {
                Console.WriteLine(ele.Key + " " + ele.Value[0]);
            }
        }

        public void display_indegree()
        {
            foreach(var ele in indegree)
            {
                Console.WriteLine(ele.Key + " " + ele.Value);
            }
        }
    }
}
