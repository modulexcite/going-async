using Newtonsoft.Json.Linq;
using Sixeyed.GoingAsync.Core;
using System.Collections.Generic;
using System.Linq;

namespace Sixeyed.GoingAsync.Steps.Enrich
{
    public class PartyEnricher
    {
		List<PartyModel> _Models;

		public PartyEnricher()
        {
            _Models = new List<PartyModel>();
            _Models.Add(new PartyModel { InternalId = "1234", LegalEntityIdentifier = "5493001RKR55V4X61F71" });
            _Models.Add(new PartyModel { InternalId = "5678", LegalEntityIdentifier = "549300O5MFEP1XJ40B46" });
            _Models.Add(new PartyModel { InternalId = "90AB", LegalEntityIdentifier = "549300OL8KL0WCQ34V31" });
            _Models.Add(new PartyModel { InternalId = "CDEF", LegalEntityIdentifier = "549300IB5Q45JGNPND58" });
        }

        public string GetInternalId(string legalEntityIdentifier)
        {
			//throw new System.ArgumentException( "Invalid LegalIdentifier: " + legalEntityIdentifier );

			return _Models.Single( m => m.LegalEntityIdentifier == legalEntityIdentifier ).InternalId;
        }
    }
}
