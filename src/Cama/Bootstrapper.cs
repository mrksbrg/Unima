﻿using System;
using System.Reflection;
using System.Windows;
using Cama.Core.Services.Project;
using Cama.Infrastructure;
using Cama.Infrastructure.Tabs;
using Cama.Module.Debug;
using Cama.Module.Loading;
using Cama.Module.Loading.Sections.Loading;
using Cama.Module.Menu;
using Cama.Module.Mutation;
using Cama.Module.Mutation.Tab;
using Cama.Module.Start;
using Cama.Module.Start.Tab;
using Cama.Sections.Shell;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Unity;

namespace Cama
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override void ConfigureServiceLocator()
        {
            base.ConfigureServiceLocator();
            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(viewType =>
            {
                var viewName = viewType.FullName;
                var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
                if (viewName.Contains("Window"))
                {
                    viewName = viewName.Remove(viewName.LastIndexOf("Window"));
                }
                else if (viewName.Contains("Dialog"))
                {
                    viewName = viewName.Remove(viewName.LastIndexOf("Dialog"));
                }
                else if (viewName.Contains("View"))
                {
                    viewName = viewName.Remove(viewName.LastIndexOf("View"));
                }
                viewName = viewName.Replace("View", "ViewModel");
                var viewModelName = $"{viewName}ViewModel, {viewAssemblyName}";
                return Type.GetType(viewModelName);
            });
        }

        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<ShellWindow>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureModuleCatalog()
        {
            AddModule(typeof(StartModule), InitializationMode.WhenAvailable);
            AddModule(typeof(MutationModule), InitializationMode.OnDemand);
            AddModule(typeof(DebugModule), InitializationMode.WhenAvailable);
            AddModule(typeof(MenuModule), InitializationMode.WhenAvailable);
            AddModule(typeof(LoadingModule), InitializationMode.WhenAvailable);
        }

        private void AddModule(Type type, InitializationMode mode)
        {
            ModuleCatalog.AddModule(new ModuleInfo
            {
                ModuleName = type.Name,
                ModuleType = type.AssemblyQualifiedName,
                InitializationMode = mode
            });
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            Container.RegisterType<IMainTabContainer, TabContainer>();
            Container.RegisterType<IMutationModuleTabOpener, MutationTabOpener>();
            Container.RegisterType<IStartModuleTabOpener, StartModuleTabOpener>();
            Container.RegisterType<ICreateCamaProjectService, CamaProjectService>();
            Container.RegisterType<IOpenCamaProjectService, CamaProjectService>();

            // Ugly, should be in the loading module...
            Container.RegisterType<LoadingViewModel>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ILoadingDisplayer, LoadingViewModel>(new ContainerControlledLifetimeManager());
        }
    }
}
