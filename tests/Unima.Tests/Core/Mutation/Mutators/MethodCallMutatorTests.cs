﻿using Microsoft.CodeAnalysis.CSharp;
using NUnit.Framework;
using Unima.Core.Creator.Mutators;

namespace Unima.Tests.Core.Mutation.Mutators
{
    [TestFixture]
    public class MethodCallMutatorTests
    {
        [Test]
        public void GetMutatedDocument_WhenCallingOnAMethodWithoutReturnValue_ShouldReplaceWithEmptyStatement()
        {
            var tree = SyntaxFactory.ParseSyntaxTree(@"
                    class C { 
                        public void FirstMethod() { Hello(); } 
                    }");
            var root = tree.GetRoot();

            var mutator = new MethodCallMutator();
            var doc = mutator.GetMutatedDocument(root, null);

            Assert.AreEqual(";", doc[0].MutationDetails.Mutation.ToString());
        }

        [Test]
        public void GetMutatedDocument_WhenCallingOnAMethodWithoutReturnValueOnAnObject_ShouldReplaceWithEmptyStatement()
        {
            var tree = SyntaxFactory.ParseSyntaxTree(@"
                    class C { 
                        public void FirstMethod() { myObject.Hello(); } 
                    }");
            var root = tree.GetRoot();

            var mutator = new MethodCallMutator();
            var doc = mutator.GetMutatedDocument(root, null);

            Assert.AreEqual(";", doc[0].MutationDetails.Mutation.ToString());
        }

        [Test]
        public void GetMutatedDocument_WhenCallingOnAMethodWithReturnValue_ShouldNotReplaceWithEmptyStatement()
        {
            var tree = SyntaxFactory.ParseSyntaxTree(@"
                    class C { 
                        public void FirstMethod() { var i = Hello(); } 
                    }");
            var root = tree.GetRoot();

            var mutator = new MethodCallMutator();
            var doc = mutator.GetMutatedDocument(root, null);

            Assert.AreEqual(0, doc.Count);
        }

        [Test]
        public void GetMutatedDocument_WhenCallingOnAMethodOnAnObjectWithReturnValue_ShouldNotReplaceWithEmptyStatement()
        {
            var tree = SyntaxFactory.ParseSyntaxTree(@"
                    class C { 
                        public void FirstMethod() { var i = myObject.Hello(); } 
                    }");
            var root = tree.GetRoot();

            var mutator = new MethodCallMutator();
            var doc = mutator.GetMutatedDocument(root, null);

            Assert.AreEqual(0, doc.Count);
        }

        [Test]
        public void GetMutatedDocument_WhenCallingOnAMethodInsideOtherMethodCall_ShouldNotReplaceWithEmptyStatement()
        {
            var tree = SyntaxFactory.ParseSyntaxTree(@"
                    class C { 
                        public void FirstMethod() { var i = m.CallMe(Hello()); var h = new MyObject(Hello(1)); } 
                    }");
            var root = tree.GetRoot();

            var mutator = new MethodCallMutator();
            var doc = mutator.GetMutatedDocument(root, null);

            Assert.AreEqual(0, doc.Count);
        }
    }
}
