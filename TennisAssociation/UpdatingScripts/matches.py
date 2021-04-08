import requests
import unicodedata
import os
import shutil
from bs4 import BeautifulSoup
import codecs
import datetime
from datetime import timedelta, date

def get_matches_for_day(date):
    d = str(date)
    d = datetime.datetime.strptime(d, "%Y-%m-%d").strftime("%m/%d/%Y")
    print(d)
    month, day, year = d.split("/")

    url = 'https://www.tennisexplorer.com/next/?type=atp-single&year=' + year + '&month=' + month + '&day=' + day
    headers = {'User-Agent': 'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.102 Safari/537.36'}
    result = requests.get(url, headers=headers)
    html = result.content.decode()

    parsed_html = BeautifulSoup(html, "html.parser")

    mydivs = parsed_html.findAll("td", {"class": ["t-name", "h2h", "result"]})

    tournament = ""
    h2h = []
    result = []
    players = []
    for info in mydivs:
        if (info.text.strip() == "Main tournaments"):
            if tournament != "" and len(players) == 2:
                match = [tournament]
                match += players
                if (h2h == []):
                    match.append("")
                    match.append("")
                else:
                    match += h2h
                if (result == []):
                    match.append("")
                    match.append("")
                else:
                    match += result
                match.append(d)
                matchesFile.writelines(",".join(match))
                matchesFile.write("\n")
            break

        if info['class'] == ['t-name']:
            a = info.find("a")
            if (a != None and a['href'].find("/player") == 0):
                # print("Player ", info.text.strip())
                if tournament != "" and len(players) == 2:
                    match = [tournament]
                    match += players
                    if (h2h == []):
                        match.append("")
                        match.append("")
                    else:
                        match += h2h
                    if (result == []):
                        match.append("")
                        match.append("")
                    else:
                        match += result
                    match.append(d)
                    matchesFile.writelines(",".join(match))
                    matchesFile.write("\n")
                    h2h = []
                    players = []
                    result = []
                players.append(info.text.strip())
            else:
                # print("Tournament ", info.text.strip())
                if tournament != "":
                    match = [tournament]
                    match += players
                    if (h2h == []):
                        match.append("")
                        match.append("")
                    else:
                        match += h2h
                    if (result == []):
                        match.append("")
                        match.append("")
                    else:
                        match += result
                    match.append(d)
                    matchesFile.writelines(",".join(match))
                    matchesFile.write("\n")
                    h2h = []
                    players = []
                    result = []
                tournament = info.text.strip()

        if info['class'] == ['h2h']:
            # print("h2h ", info.text.strip())
            h2h.append(info.text.strip())

        if info['class'] == ['result']:
            # print("result ", info.text.strip())
            result.append(info.text.strip())

matchesFile = open("matches.txt","w")
matchesFile.writelines("tournament,fristPlayer,secondPlayer,headToHeadFirst,headToHeadSecond,resultFirst,resultSecond,date")
matchesFile.write("\n")

for day in range(30):
    get_matches_for_day(date.today()- timedelta(days=day))

matchesFile.close()
