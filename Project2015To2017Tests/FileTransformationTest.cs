using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Project2015To2017.Reading;
using Project2015To2017.Transforms;
using static Project2015To2017.Definition.Project;

namespace Project2015To2017Tests
{
	[TestClass]
    public class FileTransformationTest
    {
        [TestMethod]
        public void TransformsFiles()
        {
            var project = new ProjectReader().Read("TestFiles\\fileinclusion.testcsproj");
            var transformation = new FileTransformation();

	        var progress = new Progress<string>(x => { });

            transformation.Transform(project, progress);

            Assert.AreEqual(6, project.IncludeItems.Count);

            Assert.AreEqual(1, project.IncludeItems.Count(x => x.Name == XmlNamespace + "Compile"));
            Assert.AreEqual(2, project.IncludeItems.Count(x => x.Name == "Compile"));
            Assert.AreEqual(2, project.IncludeItems.Count(x => x.Name == "Compile" && x.Attribute("Update") != null));
            Assert.AreEqual(1, project.IncludeItems.Count(x => x.Name == XmlNamespace + "EmbeddedResource")); // #73 include things that are not ending in .resx
            Assert.AreEqual(0, project.IncludeItems.Count(x => x.Name == XmlNamespace + "Content"));
            Assert.AreEqual(2, project.IncludeItems.Count(x => x.Name == XmlNamespace + "None"));
        }
    }
}
