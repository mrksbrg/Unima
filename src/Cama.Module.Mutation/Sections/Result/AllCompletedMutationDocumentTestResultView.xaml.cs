﻿using System.Collections.Generic;
using System.Windows.Controls;
using Cama.Core.Mutation.Models;
using Cama.Core.Report.Cama;

namespace Cama.Module.Mutation.Sections.Result
{
    /// <summary>
    /// Interaction logic for AllCompletedMutationDocumentTestResult.xaml
    /// </summary>
    public partial class AllCompletedMutationDocumentTestResultView : TabItem
    {
        public AllCompletedMutationDocumentTestResultView(IList<MutationDocumentResult> completedMutations)
        {
            InitializeComponent();

            var viewModel = DataContext as AllCompletedMutationDocumentTestResultViewModel;
            viewModel.Initialize(completedMutations);
        }
    }
}
