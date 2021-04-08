import requests
import unicodedata
import os
import shutil
from bs4 import BeautifulSoup
import codecs
import datetime
from datetime import timedelta, date

def get_main_tournaments():
    tournaments = []

    url = 'https://www.tennisexplorer.com/calendar/atp-men/'
    headers = {'User-Agent': 'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.102 Safari/537.36'}
    result = requests.get(url, headers=headers)
    html = result.content.decode()

    parsed_html = BeautifulSoup(html, "html.parser")

    mydivs = parsed_html.findAll("th", {"class": ["t-name"]})

    get_data = False

    for info in mydivs:
        a = info.find("a")
        tournaments.append((info.text.strip(), a['href']))

    return tournaments

def get_tournament_info(tournament):
    details_dict = {}
    details = []
    tournament_name, tournament_url = tournament
    url = 'https://www.tennisexplorer.com' + tournament_url
    headers = {'User-Agent': 'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.102 Safari/537.36'}
    result = requests.get(url, headers=headers)
    html = result.content.decode()

    parsed_html = BeautifulSoup(html, "html.parser")

    mydivs = parsed_html.find("table", {"class": ["result moneydetails"]})

    if (mydivs != None):
        mydivs = mydivs.findAll("td", {"class": ["round", "prize", "points"]})
        for info in mydivs:
            details.append(info.text.strip())

        details = [details[n:n+3] for n in range(0, len(details), 3)][1:]
        for detail in details:
            prize = detail[1].replace(',','')[:-2]
            details_dict[detail[0]] = prize + ',' + detail[2]

    return details_dict

tournamentsFile = open("tournaments.txt","w")
tournamentsFile.writelines("tournament,1. round prize,1. round points,2. round prize,2. round points,3. round prize,3. rount points,round of 16 prize,round of 16 points,quarterfinal prize,quarterfinal points,semifinal prize,semifinal points,final prize,final points,winner prize,winner points")
tournamentsFile.write("\n")

def get_data(d, key):
    if key in d:
        return d[key]
    else:
        return ","

tournaments = get_main_tournaments()
for tournament in tournaments:
    print(tournament)
    details = get_tournament_info(tournament)
    row = ""
    row += tournament[0] + "," + get_data(details,'1. round') + "," + get_data(details,'2. round') + "," + get_data(details,'3. round') + ","  + get_data(details,'round of 16') + "," + get_data(details,'quarterfinal') + "," +  get_data(details,'semifinal') + "," + get_data(details,'final') + "," +  get_data(details,'winner')
    tournamentsFile.writelines(row)
    tournamentsFile.write("\n")

    
tournamentsFile.close()
