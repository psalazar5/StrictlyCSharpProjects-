try
{
    //Gather input from user 
    Console.WriteLine("Please enter a phrase to convert to an acronym : ");
    string input = Console.ReadLine();

    // Optional exclusions 
    string exclusions = input.Replace("and", "").Replace("of", "").Replace("urmom", "");
    string[] wordArr = exclusions.Split(' '); //create an array of words from the sentence then split splits up the whole string up by whatever character we specify 
    Console.WriteLine(wordArr.Length);

    //Loop through sentence 
    string acronym = "";
    for(int i = 0; i < wordArr.Length; i++)
    {
        if (wordArr[i] != "") //if wordArr[i] is not equal to a blank string then we can process the word, but if it is then we can ignore it and loop again 
        {
            acronym += wordArr[i][0];
        }
    }
    acronym = acronym.ToUpper();
    Console.WriteLine("Your acronym is: " + acronym);
}
catch (Exception e)
{
    Console.WriteLine(e.ToString());
}