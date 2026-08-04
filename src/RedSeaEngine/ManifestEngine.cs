using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Xml;
using System.Xml.Schema;


namespace OceanApocalypse.RedSea;

public class ManifestEngine : IEngine
{
	private XmlReaderSettings settings = new();
	private readonly List<Error> errors = [];
	public string RunnerName { get; }

	public ManifestEngine(string runnerName)
	{
		RunnerName = runnerName;

		settings.Schemas.Add("http://oceanapocalypse.org/schemas", "red-sea-manifest.xsd");
		settings.ValidationType = ValidationType.Schema;
		settings.ValidationEventHandler += SchemaValidationEventHandler;
	}

	public IEnumerable<Target> GetTargets(string uri)
	{
		var reader = XmlReader.Create(uri, settings);
		List<Target> targets = [];

		bool parsingTarget = false;
		string? nameAttribute = null;
		string? osNameAttribute = null;
		string? minVersionAttribute = null;
		string? maxVersionAttribute = null;
		string? osBuildNumberAttribute = null;
		string? os = null;

		while (reader.Read())
		{
			if (reader.IsStartElement())
			{
				switch (reader.Name)
				{
					case "Target":
						/* TODO
						if (parsingTarget)
							targets.Add(new(nameAttribute, ));
						*/

						nameAttribute = reader["Name"];
						break; // todo

					default:
						throw new NotImplementedException();

				}
			}
		}

		throw new NotImplementedException(); //todo
	}

	public Result<string> Evaluate(string uri)
	{
		// todo
		throw new NotImplementedException();
	}

	public void Dispose()
	{
		settings.ValidationEventHandler -= SchemaValidationEventHandler;
		GC.SuppressFinalize(this);
	}

	private void SchemaValidationEventHandler(object? sender, ValidationEventArgs e)
	{
		if (e.Severity == XmlSeverityType.Warning)
			errors.Add(new(e.Message, 1, Severity.Warning));

		else if (e.Severity == XmlSeverityType.Error)
			errors.Add(new(e.Message, 1, Severity.Error));
	}
}
