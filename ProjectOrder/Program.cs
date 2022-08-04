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
                if (lines[0].Contains('(') || lines[0].Contains(')'))
                {
                    throw new InvalidInputFormatException("Invalid Input Format. Both Projects and dependencies on same line");
                }
                else
                {
                    Console.WriteLine("No dependency given. Projects can be run in any order");
                    return;
                }
                
            }
            
            string projects = String.Empty;
            string dependencies = String.Empty;

            projects = lines[0];
            dependencies = lines[1];
            
            Console.WriteLine("Projects - " + projects);
            Console.WriteLine("Dependencies - " + dependencies);

            ProjectDependency projectDependency = new ProjectDependency(projects, dependencies);
            List<string> project_order = projectDependency.sort_projects();
            try
            {
                Console.Write("Projects Order - ");
                foreach (string project in project_order)
                {
                    Console.Write(project + " ");
                }
                Console.WriteLine();
            }
            catch (CircularDependencyException cdex)
            {
                Console.WriteLine(cdex.Message.ToString());
            }
            catch (SelfDependencyException sdex)
            {
                Console.WriteLine(sdex.Message.ToString());
            }
            catch (InvalidInputFormatException ifex)
            {
                Console.WriteLine(ifex.Message.ToString());
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