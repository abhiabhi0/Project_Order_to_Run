using ProjectOrder;

class Program
{

    static void Main(string[] args)
    {
        try
        {
            string[] lines = System.IO.File.ReadAllLines(@"D:\Project_Order_to_Run\input_file.txt");

            List<Batch> batches = new List<Batch>();
            ProjectDependency obj = new ProjectDependency();
            string projects = String.Empty;
            string dependencies = String.Empty;

            for (int i = 0; i < lines.Count(); i+=2)
            {
                // Use a tab to indent each line of the file.
                //Console.WriteLine("\t" + line);
   
                projects = lines[i];
                if (i+1 < lines.Count())
                {
                    dependencies = lines[i + 1];
                }
               
                batches.Add(new Batch(projects, dependencies));
                projects = String.Empty;
                dependencies = String.Empty;
            }


            foreach (Batch batch in batches)
            {
                Console.Write("Projects - ");
                batch.display_projects();
                Console.Write("Dependencies - ");
                batch.display_dependencies();
                Console.Write("Projects Order - ");
                batch.find_project_order();
                batch.display_project_order();
                Console.WriteLine("\n-------------------------------");
            }

            //if (index % 2 != 0)
            //{
            //    throw new InvalidInputFormatException("Input is of Invalid Format");
            //}
            //else
            //{

            //}

            //obj.display_dependencies();
            //obj.display_indegree();
            //List<string> project_order = obj.sort_projects();
            //Console.WriteLine("Order : ");
            //foreach (string proj in project_order)
            //{
            //    Console.WriteLine(proj);
            //}
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
        catch (NotResolvedException nrex)
        {
            Console.WriteLine(nrex.Message.ToString());
            Console.WriteLine("Sorry! can't run any project in any order");
        }
        catch (InvalidInputFormatException ifex)
        {
            Console.WriteLine(ifex.Message.ToString());
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message.ToString());
        }
        
    }
}