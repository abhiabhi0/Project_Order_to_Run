using System.Configuration;
using System.Collections.Specialized;
using ProjectOrder;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            string input_file_path = String.Empty;
            input_file_path = ConfigurationManager.AppSettings.Get("input_file_path");
            string[] lines = System.IO.File.ReadAllLines(input_file_path);

            if (lines.Length == 0)
            {
                throw new EmptyInputFileException("The Input File is Empty!");
            }

            if (lines.Length == 1)
            {
                throw new InvalidInputFormatException("Invalid Input Format");
            }
            
            string projects = String.Empty;
            string dependencies = String.Empty;

            projects = lines[0];
            dependencies = lines[1];
            ProjectDependency projectDependency = new ProjectDependency(projects, dependencies);
            List<string> project_order = projectDependency.sort_projects();

            try
            {
                Console.WriteLine("Projects - " + projects);
                Console.WriteLine("Dependencies - " + dependencies);
                Console.Write("Projects Order - ");
                foreach (string project in project_order)
                {
                    Console.Write(project + " ");
                }
                Console.WriteLine();
            }
            catch (NotResolvedException nrex)
            {
                Console.WriteLine(nrex.Message.ToString());
            }

            return;
        }
        catch (InvalidInputFormatException ifex)
        {
            Console.WriteLine(ifex.Message.ToString());
        }
        catch (EmptyInputFileException eifex)
        {
            Console.WriteLine(eifex.Message.ToString());
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message.ToString());
        }
    }
}