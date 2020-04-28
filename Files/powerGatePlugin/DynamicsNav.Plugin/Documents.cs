using System;
using System.Collections.Generic;
using System.Data.Services.Common;
using System.IO;
using System.Linq;
using System.ServiceModel.Web;
using DynamicsNav.Plugin.SOAP.RecordLink;
using powerGateServer.SDK;

namespace DynamicsNav.Plugin
{
    [DataServiceKey("Number", "FileName")]
    [DataServiceEntity]
    public class Document : Streamable
    {
        public string Number { get; set; }
        public string Description { get; set; }

        public override string GetContentType()
        {
            return ContentTypes.Application.Pdf;
        }
    }

    public class Documents : ServiceMethod<Document>, IStreamableServiceMethod<Document>
    {
        public override string Name => "Documents";

        public enum LinkType
        {
            Link = 0,
            Note = 1
        }

        public override IEnumerable<Document> Query(IExpression<Document> expression)
        {
            var results = new List<Document>();

            if (expression.Where.Any(b => b.PropertyName.Equals("Number")))
            {
                var number = expression.Where.FirstOrDefault(w => w.PropertyName.Equals("Number"));
                if (number != null && number.Value != number && number.Value.ToString() != "")
                {
                    var links = GetRecordLinks(number.Value.ToString());
                    foreach (var link in links)
                    {
                        results.Add(new Document
                        {
                            Number = number.Value.ToString(),
                            FileName = Path.GetFileName(link.Url1),
                            Description = link.Content
                        });
                    }
                }
            }

            return results;
        }

        public static List<RecordLink> GetRecordLinks(string number)
        {
            var endpoint = WebService.GetServiceEndpoint<RecordLink_PortChannel>();
            var client = new RecordLink_PortClient(endpoint.Binding, endpoint.Address);

            var links = new RecordLinks();
            client.ReadLinks($"Item: {number}", WebService.Company, (int)LinkType.Link, ref links);
            return links.RecordLink.ToList();
        }

        public override void Create(Document entity)
        {
            var endpoint = WebService.GetServiceEndpoint<RecordLink_PortChannel>();
            var client = new RecordLink_PortClient(endpoint.Binding, endpoint.Address);

            var directory = Path.Combine(WebService.FileStorageLocation, entity.Number).ReplaceInvalidPathChars();
            var fullFileName = Path.Combine(directory, entity.FileName.ReplaceInvalidFileNameChars());

            var id = 0;
            client.CreateLink($"Item: {entity.Number}", WebService.Company, (int)LinkType.Link, entity.Description, fullFileName, ref id);
        }

        public override void Update(Document entity)
        {
            var directory = Path.Combine(WebService.FileStorageLocation, entity.Number).ReplaceInvalidPathChars();
            var fullFileName = Path.Combine(directory, entity.FileName.ReplaceInvalidFileNameChars());

            var links = GetRecordLinks(entity.Number);
            var link = links.SingleOrDefault(l => l.Url1.Equals(fullFileName));
            if (link != null)
            {
                var endpoint = WebService.GetServiceEndpoint<RecordLink_PortChannel>();
                var client = new RecordLink_PortClient(endpoint.Binding, endpoint.Address);

                client.ModifyLink(link.LinkId, entity.Description, fullFileName);
            }
        }

        public override void Delete(Document entity)
        {
            var directory = Path.Combine(WebService.FileStorageLocation, entity.Number).ReplaceInvalidPathChars();
            var fullFileName = Path.Combine(directory, entity.FileName.ReplaceInvalidFileNameChars());

            var links = GetRecordLinks(entity.Number);
            var link = links.SingleOrDefault(l => l.Url1.Equals(fullFileName));
            if (link != null)
            {
                var endpoint = WebService.GetServiceEndpoint<RecordLink_PortChannel>();
                var client = new RecordLink_PortClient(endpoint.Binding, endpoint.Address);

                client.DeleteLink(link.LinkId);
            }
        }

        public IStream Download(Document entity)
        {
            if (WebOperationContext.Current != null)
                WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");

            var directory = Path.Combine(WebService.FileStorageLocation, entity.Number).ReplaceInvalidPathChars();
            var fullFileName = Path.Combine(directory, entity.FileName.ReplaceInvalidFileNameChars());

            if (WebOperationContext.Current != null)
                WebOperationContext.Current.OutgoingResponse.Headers["Content-Disposition"] = $"filename={Path.GetFileName(fullFileName)}";

            return new powerGateServer.SDK.Streams.FileStream(fullFileName);
        }

        public void Upload(Document entity, IStream stream)
        {
            if (entity.Mode == TransactionMode.Update)
                DeleteStream(entity);

            try
            {
                var directory = Path.Combine(WebService.FileStorageLocation, entity.Number).ReplaceInvalidPathChars();
                var fullFileName = Path.Combine(directory, entity.FileName.ReplaceInvalidFileNameChars());

                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                using (var fs = File.Create(fullFileName))
                {
                    stream.Source.CopyTo(fs);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void DeleteStream(Document entity)
        {
            var directory = Path.Combine(WebService.FileStorageLocation, entity.Number).ReplaceInvalidPathChars();
            var fullFileName = Path.Combine(directory, entity.FileName.ReplaceInvalidFileNameChars());

            try
            {
                File.Delete(fullFileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }

    public static class PathAndFileNameMethods
    {
        public static string ReplaceInvalidFileNameChars(this string s)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
                s = s.Replace(c, '_');
            return s;
        }

        public static string ReplaceInvalidPathChars(this string s)
        {
            foreach (char c in Path.GetInvalidPathChars())
                s = s.Replace(c, '_');
            return s;
        }
    }
}