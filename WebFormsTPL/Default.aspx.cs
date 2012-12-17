using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebFormsTPL
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            DateTime start = DateTime.Now;
            int numberOfObjects;
            int.TryParse(txtNumberOfThings.Text, out numberOfObjects);

            //sequential production code example
            //List<Builder> results = Builder.LoadBuilders(numberOfObjects);

            //attempt at using parallelism to load these quicker
            //List<Builder> results = Builder.LoadBuildersInParallel(numberOfObjects);

            //attempt at using tasks to load these quicker
            //List<Builder> results = Builder.LoadBuildersWithTasks(numberOfObjects);

            //gvResults.DataSource = results;
            //gvResults.DataBind();
            //DateTime end = DateTime.Now;
            //lblRunTime.Text = (end - start).TotalSeconds.ToString();

            RegisterAsyncTask(new PageAsyncTask(GetData));
            //ASYNC tasks
            Task<Builder>[] builderLoadTasks = Enumerable.Range(0, numberOfObjects).Select(n => LoadBuilderAsync(n)).ToArray();
            Task.Factory.ContinueWhenAll<Builder>(builderLoadTasks, delegate
            {
                IEnumerable<Builder> results = builderLoadTasks.Select(builderLoadTask => builderLoadTask.Result);
                gvResults.DataSource = results;
                gvResults.DataBind();
                DateTime end = DateTime.Now;
                lblRunTime.Text = (end - start).TotalSeconds.ToString();
            });
        }

        Task GetData()
        {
            
        }
        Task<Builder> LoadBuilderAsync(int i)
        {
            string s = "Builder " + i;
            //Builder newBuilder = new Builder {Name = s, Status = s};
            var tcs = new TaskCompletionSource<Builder>();
           
            Task<string> dataTask = LoadData(s);
            dataTask.ContinueWith(delegate
            {
                string data = dataTask.Result;
                var result = new Builder { Name = s, Status = data };
                tcs.SetResult(result);
            });
            return tcs.Task;
        }

        static Task<string> LoadData(string identifier)
        {
            var tcs = new TaskCompletionSource<string>();
            var t = new System.Timers.Timer(1000);
            ElapsedEventHandler lambda = null; lambda = delegate
            {
                t.Elapsed -= lambda;
                tcs.TrySetResult("loaded " + identifier);
            };
            t.Elapsed += lambda;
            t.Start();
            return tcs.Task;
        }
    }
}