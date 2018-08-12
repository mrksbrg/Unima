﻿using System.Collections.Generic;
using System.Windows.Controls;
using Cama.Core.Models.Mutation;

namespace Cama.Module.Mutation.Sections.TestRun
{
    /// <summary>
    /// Interaction logic for TestRunView
    /// </summary>
    public partial class TestRunView : TabItem
    {
        public TestRunView(IList<MutatedDocument> documents, CamaConfig config)
        {
            InitializeComponent();

            var dataContext = DataContext as TestRunViewModel;
            dataContext.SetDocuments(documents, config);
        }
    }
}
