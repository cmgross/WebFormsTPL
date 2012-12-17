using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AsyncWebForm
{
    public partial class _Default : Page
    {
        private int _numberOfObjects;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {            
            int.TryParse(txtNumberOfThings.Text, out _numberOfObjects);
            RegisterAsyncTask(new PageAsyncTask(GetDataFromServicesAsync));
        }

        async Task GetDataFromServicesAsync()
        {
            var startThread = Thread.CurrentThread.ManagedThreadId;
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            var task1 = GetCustomers(_numberOfObjects);
            var task2 = GetProducts(_numberOfObjects);

            await Task.WhenAll(task1, task2);

            List<string> data1 = task1.Result;
            List<string> data2 = task2.Result;

            stopWatch.Stop();
            var endThread = Thread.CurrentThread.ManagedThreadId;
            lblThreadDetails.Text = string.Format("<h2>Started on thread: {0}<br /> Finished on thread: {1}</h2>", startThread, endThread);
            lblLoading.Text = string.Format("<h2>Retrieved {0} customers and {1} products in {2} seconds.</h2>",
                                         data1.Count, data2.Count, stopWatch.Elapsed.TotalSeconds);
        }

        async Task<List<string>> GetCustomers(int numberOfCustomers)
        {
            await Task.Delay(5000);
            var customers = new List<string>();
            for (var i = 0; i < numberOfCustomers; i++)
            {
                customers.Add("Customer" + i);
            }
            return customers;
        }
        async Task<List<string>> GetProducts(int numberOfProducts)
        {
            await Task.Delay(5000);
            var customers = new List<string>();
            for (var i = 0; i < numberOfProducts; i++)
            {
                customers.Add("Products" + i);
            }
            return customers;
        }
    }
}