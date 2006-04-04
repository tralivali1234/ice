// **********************************************************************
//
// Copyright (c) 2003-2006 ZeroC, Inc. All rights reserved.
//
// This copy of Ice is licensed to you under the terms described in the
// ICE_LICENSE file included in this distribution.
//
// **********************************************************************

using System;

public class Client
{
    public static int run(string[] args, Ice.Communicator communicator)
    {
        Test.RetryPrx retry = AllTests.allTests(communicator);
        retry.shutdown();
        return 0;
    }

    public static void Main(string[] args)
    {
        int status = 0;
        Ice.Communicator communicator = null;

        try
        {
	    Ice.Properties properties = Ice.Util.getDefaultProperties(ref args);

	    //
	    // For this test, we want to disable retries.
	    //
	    properties.setProperty("Ice.RetryIntervals", "-1");

	    //
	    // This test kills connections, so we don't want warnings.
	    //
	    properties.setProperty("Ice.Warn.Connections", "0");
	    
            communicator = Ice.Util.initialize(ref args);
            status = run(args, communicator);
        }
        catch(Ice.LocalException ex)
        {
            Console.Error.WriteLine(ex);
            status = 1;
        }

        if(communicator != null)
        {
            try
            {
                communicator.destroy();
            }
            catch(Ice.LocalException ex)
            {
	        Console.Error.WriteLine(ex);
                status = 1;
            }
        }

	Environment.Exit(status);
    }
}
