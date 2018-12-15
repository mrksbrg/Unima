﻿using System;
using System.Collections.Generic;
using System.Linq;
using Cama.Core.Execution.Compilation;
using Cama.Core.Execution.Result;
using Cama.Core.Location;
using Cama.Core.Util;

namespace Cama.Core
{
    public class MutationDocumentResult
    {
        public MutationDocumentResult()
        {
            FailedTests = new List<TestResult>();
        }

        public Guid Id { get; set; }

        public string ProjectName { get; set; }

        public string FileName { get; set; }

        public string Orginal { get; set; }

        public string FullOrginal { get; set; }

        public string Mutation { get; set; }

        public string FullMutation { get; set; }

        public bool Survived => !FailedTests.Any() && (CompilationResult != null && CompilationResult.IsSuccess);

        public CompilationResult CompilationResult { get; set; }

        public int TestsRunCount { get; set; }

        public IList<TestResult> FailedTests { get; set; }

        public MutationLocationInfo Location { get; set; }

        public string MutationName { get; set; }

        public string UnexpectedError { get; set; }

        public string Hash { get; set; }

        public void GenerateHash()
        {
            Hash = HashHelper.CreateMD5Hash(
                ProjectName +
                FileName +
                Orginal +
                Mutation);
        }
    }
}
