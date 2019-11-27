//==========================================================================================
//
//		OpenNETCF.Windows.Forms.PasswordDeriveBytes
//		Copyright (c) 2003, OpenNETCF.org
//
//		This library is free software; you can redistribute it and/or modify it under 
//		the terms of the OpenNETCF.org Shared Source License.
//
//		This library is distributed in the hope that it will be useful, but 
//		WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or 
//		FITNESS FOR A PARTICULAR PURPOSE. See the OpenNETCF.org Shared Source License 
//		for more details.
//
//		You should have received a copy of the OpenNETCF.org Shared Source License 
//		along with this library; if not, email licensing@opennetcf.org to request a copy.
//
//		If you wish to contact the OpenNETCF Advisory Board to discuss licensing, please 
//		email licensing@opennetcf.org.
//
//		For general enquiries, email enquiries@opennetcf.org or visit our website at:
//		http://www.opennetcf.org
//
//==========================================================================================

using System;
using System.Web.Services.Protocols;
using OpenNETCF.Web.Services2.Dime;

namespace OpenNETCF.Web.Services2.Dime
{
	public class DimeWrap : SoapHttpClientProtocol, IDimeAttachmentContainer
	{
		public DimeWrap() : base()
		{}

		DimeAttachmentCollection requestAttachments;
		DimeAttachmentCollection responseAttachments;

		// IDimeAttachmentContainer.RequestAttachments
		public DimeAttachmentCollection RequestAttachments 
		{ 
			get 
			{
				if (requestAttachments == null) 
					requestAttachments = new DimeAttachmentCollection();
				return requestAttachments;
			}
		}

		// IDimeAttachmentContainer.ResponseAttachments
		public DimeAttachmentCollection ResponseAttachments 
		{ 
			get 
			{
				if (responseAttachments == null) 
					responseAttachments = new DimeAttachmentCollection();
				return responseAttachments;
			}
		}
	}
}
