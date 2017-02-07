using System;
using ConsoleApp.DependencyResolution;
using CTOnline.OpenServicesFx;
using CTOnline.OpenServicesFx.Reference;
using SampleServicesLibrary;
using StructureMap;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Bootstraper.InitializeLoggingAndDatadog("LightAppServicesFx-Console");

            IActionExecutionContext context = new ActionExecutionContext();
            ProgramArgsActionContextExtensions.ResolveActionExecutionContext(args, context);

            IContainer container = IoC.Initialize();
            // Overrides for IoC
            container.Inject<IActionExecutionContext>(context);

            var service = container.GetInstance<IValuesService>();
            var response = service.GetAllItems();
            if (response.HasError) {
                Console.WriteLine("Status {0}, Message {1}",response.Status,response.Message);
            }
            else
                foreach (var value in response.Content)
                    Console.WriteLine("{0} {1}", value.Id, value.Text);

            var responseSingle = service.GetItemById(99);
            if (responseSingle.HasError)
            {
                Console.WriteLine("Status {0}, Message {1}", responseSingle.Status, responseSingle.Message);
                foreach (var err in responseSingle.Errors)
                    Console.WriteLine(err.Description);
            }
            else
            {
                var value = responseSingle.Content;
                Console.WriteLine("{0} {1}", value.Id, value.Text);
            }

            Console.WriteLine("Press Enter to finish...");
            Console.ReadLine();
        }
    }
}
