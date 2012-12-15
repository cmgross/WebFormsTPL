using System;
using System.Collections.Generic;
using System.Linq;
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
            List<Builder> results = Builder.LoadBuilders(numberOfObjects);

            //attempt at using parallelism to load these quicker
            //List<Builder> results = Builder.LoadBuildersInParallel(numberOfObjects);

            //attempt at using tasks to load these quicker
            //List<Builder> results = Builder.LoadBuildersWithTasks(numberOfObjects);

            gvResults.DataSource = results;
            gvResults.DataBind();
            DateTime end = DateTime.Now;
            lblRunTime.Text = (end - start).TotalSeconds.ToString();
        }
    }
}