using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmsDependencyInjection
{
    interface ILevelRepository
    {
        void FetchNextLevel();
    }

    interface ILog
    {
        void WriteLog(string s);
    }
    class Log : ILog
    {
        public void WriteLog(string s)
        {

        }
    }

    class FileHandler : ILevelRepository
    {
        private readonly ILog logger;

        public FileHandler(ILog logger)
        {
            this.logger = logger;
        }
        public void FetchNextLevel() {
            logger.WriteLog("Nu hämtas new level i fil");
            //fwetch.---
        }
    }
    class DatabaseHandler : ILevelRepository
    {
        public void FetchNextLevel() { }
    }

    interface IHighScoreSaveService
    {
        void Save(int points);
    }
    class HighScoreDatabaseService : IHighScoreSaveService
    {
        public void Save(int points)
        {
            //throw new NotImplementedException();
        }

        
    }

    class Spel
    {
        private ILevelRepository levelRepository;
        private readonly IHighScoreSaveService high;

        public Spel(ILevelRepository lev, IHighScoreSaveService high)
        {
            levelRepository = lev;
            this.high = high;
        }
        public void GameLoop()
        {
            levelRepository.FetchNextLevel();
            int highscore = 99;
            high.Save(highscore);
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container();

            container.RegisterSingleton<ILog, Log>();
            container.Register<IHighScoreSaveService, HighScoreDatabaseService>();
            container.Register<ILevelRepository, FileHandler>();
            container.Register<Spel>();

            container.Verify();


            var spel = container.GetInstance<Spel>();
            spel.GameLoop();
        }
    }
}
