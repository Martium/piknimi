﻿using System;
using System.Threading;
using System.Windows.Forms;
using PikNiMi.Forms;
using PikNiMi.Repository.DependencyInjectionRepositoryClass.Repository;
using PikNiMi.Repository.SqlLite;

namespace PikNiMi
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        private const string AppUuid = "9c973b26-7a07-11ec-90d6-0242ac120003";

        private static RepositoryCreate _repositoryCreate;
        private static FakeRepository _fakeRepository;

        [STAThread]
        static void Main()
        {
            _repositoryCreate = new RepositoryCreate(new SqlLiteInitializeRepository());
            _fakeRepository = new FakeRepository();

            using (Mutex mutex = new Mutex(false, "Global\\" + AppUuid))
            {
                if (!mutex.WaitOne(0, false))
                {
                    MessageBox.Show(@"Cant Open more than One PikNiMi application");
                }

                bool isDatabaseIsInitialize = CreateDataBase();

                if (isDatabaseIsInitialize)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new MainForm());
                }
            }
        }

        private static bool CreateDataBase()
        {
            bool isDatabaseInitialize;

            try
            {
                _repositoryCreate.CreateRepositoryFile();
                _repositoryCreate.CreateRepositoryTable();
                _fakeRepository.FillTestingInfoForProduct();
                _fakeRepository.FillTestingAdditionalInfoForProduct();

                isDatabaseInitialize = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                isDatabaseInitialize = false;
            }

            return isDatabaseInitialize;
        }
    }
}
