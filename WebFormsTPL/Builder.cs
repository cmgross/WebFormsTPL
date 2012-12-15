using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace WebFormsTPL
{
    public class Builder
    {
        public string Name { get; set; }
        public string Status { get; set; }

        //Production mock, objects are loaded from database sequentially and some operations are done to them
        public static List<Builder> LoadBuilders(int numberOfBuilders)
        {
            List<Builder> loadedBuilders = new List<Builder>();

            for (int i = 0; i < numberOfBuilders; i++)
            {
                loadedBuilders.Add(new Builder{ Name = "Builder" + i, Status = "Status" + i});
            }

            foreach (Builder loadedBuilder in loadedBuilders)
            {
                //simulate long loading database process, etc
                Thread.Sleep(1000);
            }
            return loadedBuilders;
        }

        public static List<Builder> LoadBuildersInParallel(int numberOfBuilders)
        {
            BlockingCollection<Builder> buildersToLoad = new BlockingCollection<Builder>();
            BlockingCollection<Builder> loadedBuilders = new BlockingCollection<Builder>();

            for (int i = 0; i < numberOfBuilders; i++)
            {
                buildersToLoad.Add(new Builder { Name = "Builder" + i, Status = "Status" + i });
            }

            Parallel.ForEach(buildersToLoad, new ParallelOptions { MaxDegreeOfParallelism = 2 }, currentBuilder =>
            {
              //database load/instansiation of objects
               Thread.Sleep(1000);
                loadedBuilders.Add(currentBuilder);
            }); 

            return loadedBuilders.ToList();
        }

        public static List<Builder> LoadBuildersWithTasks(int numberOfBuilders)
        {
            BlockingCollection<Builder> buildersToLoad = new BlockingCollection<Builder>();
            BlockingCollection<Builder> loadedBuilders = new BlockingCollection<Builder>();

            for (int i = 0; i < numberOfBuilders; i++)
            {
                buildersToLoad.Add(new Builder { Name = "Builder" + i, Status = "Status" + i });
            }
            buildersToLoad.CompleteAdding();

            Task loader1 = Task.Factory.StartNew(() =>
                {
                    foreach (Builder item in buildersToLoad.GetConsumingEnumerable())
                    {
                        Thread.Sleep(1000);
                        loadedBuilders.Add(item);
                    }
                }, TaskCreationOptions.LongRunning);

            Task loader2 = Task.Factory.StartNew(() =>
            {
                foreach (Builder item in buildersToLoad.GetConsumingEnumerable())
                {
                    Thread.Sleep(1000);
                    loadedBuilders.Add(item);
                }
            }, TaskCreationOptions.LongRunning);

            Task.WaitAll(loader1, loader2);
            return loadedBuilders.ToList();
        }
    }
}