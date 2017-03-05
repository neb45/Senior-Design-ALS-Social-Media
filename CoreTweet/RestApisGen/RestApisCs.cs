﻿using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RestApisGen
{
    static class RestApisCs
    {
        public static void Generate(IEnumerable<ApiParent> apis, TextWriter writer)
        {
            writer.Write(@"// This file was automatically generated by CoreTweet.
// Do not modify this file directly.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using CoreTweet.Core;
#if ASYNC
using System.Threading;
using System.Threading.Tasks;
#endif

#pragma warning disable RECS0163

namespace CoreTweet.Rest
{
");

            var ind = new Indent(1);
            foreach (var i in apis)
            {
                writer.WriteLine(ind + "/// <summary>");
                writer.WriteLine(ind + "/// {0}", i.Description);
                writer.WriteLine(ind + "/// </summary>");
                writer.WriteLine(ind + "public partial class {0} : ApiProviderBase", i.Name);
                writer.WriteLine(ind + "{");
                ind.Inc();
                writer.WriteLine(ind + "internal " + i.Name + "(TokensBase e) : base(e) { }");
                writer.WriteLine("");
                foreach (var j in i.Endpoints)
                {
                    if (j is RawLines)
                    {
                        foreach (var l in (j as RawLines).Lines)
                            writer.WriteLine(ind + l);
                        writer.WriteLine("");
                        continue;
                    }
                    writer.WriteLine(ind + "#if SYNC");

                    foreach (var m in j.Methods)
                    {
                        var @when = m.WhenClause;
                        if (@when != null)
                            writer.WriteLine(ind + "#if " + @when);

                        writer.WriteLine(ind + "/// <summary>");
                        foreach (var k in j.Description)
                            writer.WriteLine(ind + "/// <para>" + k + "</para>");

                        var prms = m.Params.Distinct(new ParameterNameComparer()).ToArray();

                        if (!m.HasStaticArgs)
                        {
                            if (prms.Length > 0)
                            {
                                writer.WriteLine(ind + "/// <para>Available parameters:</para>");
                                foreach (var k in prms)
                                    writer.WriteLine(ind + "/// <para>- <c>{0}</c> {1} ({2})</para>", k.Type.Replace("<", "&lt;").Replace(">", "&gt;"), k.RealName, k.Kind);
                            }
                            else
                            {
                                writer.WriteLine(ind + "/// <para>Available parameters: Nothing.</para>");
                            }
                        }
                        writer.WriteLine(ind + "/// </summary>");

                        if (m.HasStaticArgs)
                            foreach (var k in prms)
                                writer.WriteLine(ind + "/// <param name=\"{0}\">{1}.</param>", k.RealName, k.Kind);
                        else writer.WriteLine(ind + "/// <param name=\"parameters\">The parameters.</param>");
                        if (m.Definition.Contains("EnumerateMode mode"))
                            writer.WriteLine(ind + "/// <param name=\"mode\">Specify whether enumerating goes to the next page or the previous.</param>");
                        writer.WriteLine(ind + "/// <returns>{0}</returns>", j.Returns);

                        foreach (var a in j.Attributes)
                            writer.WriteLine(ind + "[{0}(\"{1}\")]", a.Item1, a.Item2);
                        writer.WriteLine(ind + m.Definition);
                        writer.WriteLine(ind + "{");
                        ind.Inc();
                        foreach (var l in m.Body)
                            writer.WriteLine(ind + l);
                        ind.Dec();
                        writer.WriteLine(ind + "}");

                        if (@when != null)
                            writer.WriteLine(ind + "#endif");

                        writer.WriteLine("");
                    }

                    writer.WriteLine(ind + "#endif");
                    writer.WriteLine(ind + "#if ASYNC");
                    writer.WriteLine("");

                    foreach (var m in j.MethodsAsync)
                    {
                        var @when = m.WhenClause;
                        if (@when != null)
                            writer.WriteLine(ind + "#if " + @when);

                        writer.WriteLine(ind + "/// <summary>");
                        foreach (var k in j.Description)
                            writer.WriteLine(ind + "/// <para>" + k + "</para>");

                        var prms = m.Params.Distinct(new ParameterNameComparer()).ToArray();

                        if (!m.HasStaticArgs)
                        {
                            writer.WriteLine(ind + "/// <para>Available parameters:</para>");
                            foreach (var k in prms)
                                writer.WriteLine(ind + "/// <para>- <c>{0}</c> {1} ({2})</para>", k.Type.Replace("<", "&lt;").Replace(">", "&gt;"), k.RealName, k.Kind);
                        }
                        writer.WriteLine(ind + "/// </summary>");

                        if (m.HasStaticArgs)
                            foreach (var k in prms)
                                writer.WriteLine(ind + "/// <param name=\"{0}\">{1}.</param>", k.RealName, k.Kind);
                        else writer.WriteLine(ind + "/// <param name=\"parameters\">The parameters.</param>");
                        if (m.TakesCancellationToken)
                            writer.WriteLine(ind + "/// <param name=\"cancellationToken\">The cancellation token.</param>");
                        writer.WriteLine(ind + "/// <returns>{0}</returns>", j.Returns);

                        writer.WriteLine(ind + m.Definition);
                        writer.WriteLine(ind + "{");
                        ind.Inc();
                        foreach (var l in m.Body)
                            writer.WriteLine(ind + l);
                        ind.Dec();
                        writer.WriteLine(ind + "}");

                        if (@when != null)
                            writer.WriteLine(ind + "#endif");

                        writer.WriteLine("");
                    }
                    writer.WriteLine(ind + "#endif");
                    writer.WriteLine("");
                }
                ind.Dec();
                writer.WriteLine(ind + "}");
                writer.WriteLine("");
            }

            writer.WriteLine('}');
        }
    }
}
