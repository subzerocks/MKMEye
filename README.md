# MKMEYE

Sell on MKM/MagicKartenMarkt (or MCM for international folks -> https://www.magiccardmarket.com) by scanning your cards with image recognition.

### But Why?

If I feel lucky and buy a collection to merge into mine I usally end up with a lot of bulk. You know these cards not really worth much. But selling it usally is a lot of work since you have to add each card and ship it. So why not skipping at least one of the parts if its possible? This is how this project started. I was searching the web upside down for a good solution and found quite a few image recognition projects, but they were all very basic. 

There was one which looked ok to me: I found this rather old project public domain project by Peter Sterm realeased on reddit. It was written in C# and had the needed basics for my needs. So I decided to pimp it a bit for my needs. This is how this started. At the moment my personal goal is to sell that much Bulk that way so that I can get some nice graded Antiquities or other vintage cards for my collection - I am still mad for a graded Sylex *_* .

### How to get it working

1.) First you need a MySQL Server, set one up:

https://dev.mysql.com/downloads/mysql/

2.) Install the .NET MySQL Conenctor (included in /Assets)

2.) Create a database and add the cards table structure (included in /Assets)

3.) Get the XLHQ MTG Images (aroudn 13 Gb + the newer set singles from Mega) 

4.) Compile and run the ImageDBCreator (inside this project) to build the pHash DB from the images. Be sure you edit the config to match it your database. You can purge the images if you are done building your DB and not plan to regenerate it, they are no longer needed. 

5.) compile the MKMEye Project

6.) Change the config and add your MKM Api Key + MySQL Details

7.) Start scanning and selling

**Donators can request a copy of my personal pHash DB.

The tool will add new cards for a price of 1982 Eur, you can adjust this in the code or you run my MKMTool to update your prices afterwards liek I do. The MKMTool can be found on my github as well.

###And finally

The development of this tool cost me a lot of time recently but it was worth it to see it done now. 

If you like this tool, you can donate me some bitcoin leftovers to my wallet here:

13Jjvvnmn6t1ytbqWTZaio1zWNVWpbcEpG

Or by me something of my amazon wishlist here:

https://www.amazon.de/registry/wishlist/PY25W6O71YIV/ref=cm_sw_em_r_mt_ws__ugkRybY0HFNYD

*Sorry I don’t have paypal.*

**If you are producing a commercial product and need some help with the MKM API or you want to integrate some of my code in your application, feel free to contact me.**


Original Magic Vision Release:
https://www.reddit.com/r/magicTCG/comments/lccrx/full_source_code_of_the_mtg_card_image/

Source for the JSON used by the scraper to get the multiverseid needed for the gatherer image download:
https://mtgjson.com/
