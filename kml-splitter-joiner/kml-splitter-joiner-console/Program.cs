using SharpKml.Dom;
using SharpKml.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kml_splitter_joiner
{
    class Program
    {
        static void Main(string[] args)
        {
            String inFile = @"C:\tmp\Test\Ferrocarril Belgrano.kml";
            String outFile = @"C:\tmp\Test\out.kml";
            KmlFile kmlFile = null;
            using (Stream fileStream = File.OpenRead(inFile))
            {
                kmlFile = KmlFile.Load(fileStream);
            }
            Kml kml = kmlFile.Root as Kml;
            Document d = kml.Feature as Document;
            Folder f = (Folder)d.Features.FirstOrDefault();
            Folder f2 = (Folder)f.Features.FirstOrDefault();

            Document doc = new Document();
            doc.Name = "MyTestKml";
            d.Schemas.ToList().ForEach(s => doc.AddSchema(s.Clone()));
            doc.AddFeature(f2.Clone());

            // This allows us to save and Element easily.
            KmlFile kmlOut = KmlFile.Create(doc, false);
            using (var stream = System.IO.File.OpenWrite(outFile))
            {
                kmlOut.Save(stream);
            }
        }
    }
}
