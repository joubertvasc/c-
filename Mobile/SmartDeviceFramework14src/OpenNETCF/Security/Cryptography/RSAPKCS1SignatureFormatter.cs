//==========================================================================================
//
//		OpenNETCF.Windows.Forms.RSAPKCS1SignatureFormatter
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
//		!!! A HUGE thank-you goes out to Casey Chesnut for supplying this class library !!!
//      !!! You can contact Casey at http://www.brains-n-brawn.com                      !!!
//
//==========================================================================================
using System;

namespace OpenNETCF.Security.Cryptography 
{ 
	
	public class RSAPKCS1SignatureFormatter : AsymmetricSignatureFormatter 
	{
	
		private RSACryptoServiceProvider rsa;
		//private HashAlgorithm hash;
		private string hashName;
	
		public RSAPKCS1SignatureFormatter () {}
	
		public RSAPKCS1SignatureFormatter (AsymmetricAlgorithm key) 
		{
			SetKey (key);
		}
	
		public override byte[] CreateSignature (byte[] rgbHash) 
		{
			if (rsa == null)
				throw new CryptographicUnexpectedOperationException ("missing key");
			if (hashName == null)
				throw new CryptographicUnexpectedOperationException ("missing hash algorithm");
			if (rgbHash == null)
				throw new ArgumentNullException ("rgbHash");

			return rsa.SignHash(rgbHash, hashName);
		}
	
		public override void SetHashAlgorithm (string strName) 
		{
			//hash = HashAlgorithm.Create (strName);
			if (strName == null)
				throw new ArgumentNullException ("strName");
			hashName = strName;
		}
	
		public override void SetKey (AsymmetricAlgorithm key) 
		{
			if (key != null) {
				rsa = (RSACryptoServiceProvider) key;
			}
		}

		public override byte[] CreateSignature (HashAlgorithm hash) 
		{
			if (hash == null)
				throw new ArgumentNullException ();
			SetHashAlgorithm (hash.ToString());
			return CreateSignature (hash.Hash);
		}
	}
}
