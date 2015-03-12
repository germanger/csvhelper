# MangerCSVHelper
Convert an heterogeneous List&lt;dynamic> to a CSV string, regardless their properties. It supports nested properties too.

## Example

Say you have an heterogeneous list of dynamics (they don't share all the same properties) and some of the items have nested properties

    List<dynamic> heterogeneousList = new List<dynamic>()
    {
        new 
        {
            name = "German",
            birthDate = new DateTime(1988, 3, 31),
            age = 28,
            height = 1.71,
        },
        new 
        {
            name = "Valentina",
            instrument = new
            {
                name = "bass",
                brand = new 
                {
                    name = "fender"
                }
            }
        },
        new 
        {
            code = "foo",
        },
    };
    
Use the helper by simply calling the static member `GetCSVString`:

    string result = MangerCSV.CSVHelper.GetCSVString(heterogeneousList);

And the resultant CSV string is:

    name;birthDate;age;height;instrument__name;instrument__brand__name;code
    German;1988-03-31;28;1.71;;;;
    Valentina;;;;bass;fender;;
    ;;;;;;foo;
    
As you can see, if an item of the list doesn't have a property, it is left in blank. Also nested properties are expressed with a "`__`" in the header (eg: `instrument__brand__name`)

## Usage

Include the MangerCSV.dll in your project

Call the static member `MangerCSV.CSVHelepr.GetCSVString`, by passing the following parameters:

* inputList: List of dynamic objects
* cultureInfo: Default is InvariantCulture. This parameter is relevant when converting decimal numbers to string.
* columnDelimiter: Default is `";"`
* dateFormat: Default is `"yyyy-MM-dd"`

## Algorithm

It works by flattening out each object of the list into an ExpandoObject (a dictionary), by using reflection. Nested properties are renamed and brought to the root of the object.

## Idea

Idea came from these JSON to CSV converters:

* http://konklone.io/json/
* https://json-csv.com/

I needed something that supported nested properties and heterogeneous objects (with different properties). I Also posted the core of this solution as an answer in `StackOverflow`:

* http://stackoverflow.com/questions/27734201/serializing-a-list-of-dynamic-objects-to-a-csv-with-servicestack-text

## TO DOs

* Maybe wrapping each item of the generated string into `""`, eg: `"foo";"bar";...`
* Do more testing. It might have bugs for weird cases.

