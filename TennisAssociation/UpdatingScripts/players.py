import requests
import unicodedata
import os
import shutil
from bs4 import BeautifulSoup
import codecs
import datetime

def strip_accents(text):
    return ''.join(char for char in unicodedata.normalize('NFKD', text) if unicodedata.category(char) != 'Mn')

dir = 'img'
if os.path.exists(dir):
    shutil.rmtree(dir)
os.mkdir(dir)

playersFile = open("players.txt","w")

url = 'https://www.tennisexplorer.com/ranking/atp-men/?page=1'
headers = {'User-Agent': 'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.102 Safari/537.36'}
result = requests.get(url, headers=headers)
html = result.content.decode()

parsed_html = BeautifulSoup(html, "html.parser")

mydivs = parsed_html.findAll("td", {"class": "t-name"})

playersFile.write('firstName,lastName,country,height,weight,birth,currentRankingSingle,bestRankingSingle,currentRankingDouble,bestRankingDouble,sex,hand')
playersFile.write("\n")

top = 50
counter = 0
for info in mydivs:
    playerInfo = []
    name = info.text.split()
    lastName = ' '.join(name[:-1])
    firsName = name[-1]
    playerInfo.append(firsName)
    playerInfo.append(lastName)

    url = 'https://www.tennisexplorer.com/' + info.find("a")['href']
    headers = {'User-Agent': 'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.102 Safari/537.36'}
    result = requests.get(url, headers=headers)
    html = result.content.decode()
    parsed_html = BeautifulSoup(html, "html.parser")
    divs = parsed_html.findAll("div", {"class": "date"})

    country = divs[0].text
    country = country[country.index(':') + 1:].strip()
    playerInfo.append(country)

    height = divs[1].text
    height = height[height.index(':') + 1:].strip()
    height = height.split()[0]
    playerInfo.append(height)

    weight = divs[1].text
    weight = weight[weight.index(':') + 1:].strip()
    weight = weight.split()[3]
    playerInfo.append(weight)

    date = divs[2].text
    date = date[date.index('(') + 1: date.index(')')].strip()
    date = datetime.datetime.strptime(date, "%d. %m. %Y").strftime("%m/%d/%Y")
    playerInfo.append(date)

    singleRank = divs[3].text
    singleRank = singleRank[singleRank.index(':') + 1:].strip()
    singleRankCurrent = singleRank.split()[0]
    singleRankCurrent = singleRankCurrent[0:-1]
    singleRankHighest = singleRank.split()[2]
    singleRankHighest = singleRankHighest[0:-1]
    playerInfo.append(singleRankCurrent)
    playerInfo.append(singleRankHighest)

    doubleRank = divs[4].text
    doubleRank = doubleRank[doubleRank.index(':') + 1:].strip()
    doubleRankCurrent = doubleRank.split()[0]
    doubleRankCurrent = doubleRankCurrent[0:-1]
    doubleRankHighest = doubleRank.split()[2]
    doubleRankHighest = doubleRankHighest[0:-1]
    
    playerInfo.append(doubleRankCurrent)
    playerInfo.append(doubleRankHighest)

    sex = divs[5].text
    sex = sex[sex.index(':') + 1:].strip()
    playerInfo.append(sex)

    hand = divs[6].text
    hand = hand[hand.index(':') + 1:].strip()
    playerInfo.append(hand)

    player = ','.join(playerInfo)
    print(player)
    img = parsed_html.find('img', alt=info.text)
    response = requests.get('https://www.tennisexplorer.com/'+img['src'])
    imgfile = open('img/'+ info.text + '.png', "wb")
    imgfile.write(response.content)
    imgfile.close()

    playersFile.writelines(player)
    playersFile.write("\n")
    counter += 1
    if counter == top:
        break



playersFile.close()
