using ProjectOrder;

class Program
{

    static void Main(string[] args)
    {
        string[] lines = System.IO.File.ReadAllLines(@"D:\ProjectOrder\input_file.txt");
        
        int index = 0;
        List<string> projects = new List<string>();
        List<string> projects_pairs = new List<string>();
        //string[] projects_pairs;

        ProjectDependency obj = new ProjectDependency();

        foreach (string line in lines)
        {
            // Use a tab to indent each line of the file.
            //Console.WriteLine("\t" + line);
            if (index == 0)
            {
                obj.find_projects(line);
            }
            else
            {
                //projects_pairs = line.Split(',').ToList<string>();
                obj.find_dependencies(line);
            }
            index++;
        }
        obj.display_dependencies();
        obj.display_indegree();
        List<string> project_order = obj.sort_projects();
        Console.WriteLine("Order : ");
        foreach (string proj in project_order)
        {
            Console.WriteLine(proj);
        }
        //Console.WriteLine("\t" + projects[1]);
        //Console.WriteLine("\t" + projects_pairs[1]);
        //for (int i = 0; i < projects.Count; ++i)
        //{
        //    projects[i] = projects[i].Trim();
        //    Console.WriteLine(projects[i]);
        //}

        //for (int i = 0; i < projects_pairs.Count; i++)
        //{
        //    projects_pairs[i] = projects_pairs[i].Trim();
        //    Console.WriteLine(projects_pairs[i]);
        //}
        return;
    }
}