import base64
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

def getPlayers(pageNumber):
    url = 'https://www.tennisexplorer.com/ranking/atp-men/?page=' + str(pageNumber)
    headers = {'User-Agent': 'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.102 Safari/537.36'}
    result = requests.get(url, headers=headers)
    html = result.content.decode()

    parsed_html = BeautifulSoup(html, "html.parser")

    mydivs = parsed_html.findAll("td", {"class": "t-name"})

    if pageNumber == 1:
        playersFile.write('firstName,lastName,country,height,weight,birth,currentRankingSingle,bestRankingSingle,currentRankingDouble,bestRankingDouble,sex,hand,img')
        playersFile.write("\n")

    counter = 0
    for info in mydivs:
        if counter == 50:
            break
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

        features = {}
        for div in divs:
            x,y = div.text.split(':')
            features[x.strip()] = y.strip()

        if 'Country' in features:
            playerInfo.append(features['Country'])
        if 'Height / Weight' in features:
            height,_,_,weight,_ = features['Height / Weight'].split()
            playerInfo.append(height)
            playerInfo.append(weight)
        else:
            playerInfo.append("")
            playerInfo.append("")
        if 'Age' in features:
            date = features['Age']
            date = date[date.index('(') + 1: date.index(')')].strip()
            date = datetime.datetime.strptime(date, "%d. %m. %Y").strftime("%m/%d/%Y")
            playerInfo.append(date)
        if 'Current/Highest rank - singles' in features:
            singleRank = features['Current/Highest rank - singles' ]
            singleRankCurrent = singleRank.split()[0]
            singleRankCurrent = singleRankCurrent[0:-1]
            singleRankHighest = singleRank.split()[2]
            singleRankHighest = singleRankHighest[0:-1]
            playerInfo.append(singleRankCurrent)
            playerInfo.append(singleRankHighest)
        if 'Current/Highest rank - doubles' in features:
            doubleRank = features['Current/Highest rank - doubles']
            doubleRankCurrent = doubleRank.split()[0]
            doubleRankCurrent = doubleRankCurrent[0:-1]
            doubleRankHighest = doubleRank.split()[2]
            doubleRankHighest = doubleRankHighest[0:-1]
            playerInfo.append(doubleRankCurrent)
            playerInfo.append(doubleRankHighest)
        if 'Sex' in features:
            sex = features['Sex']
            playerInfo.append(sex)
        if 'Plays' in features:
            hand = features['Plays']
            playerInfo.append(hand)

        img = parsed_html.find('img', alt=info.text)
        response = requests.get('https://www.tennisexplorer.com/'+img['src'])
        if (response != None):
            playerInfo.append(str(base64.b64encode(response.content), 'utf-8'))
        imgfile = open('img/'+ info.text + '.png', "wb")
        imgfile.write(response.content)
        imgfile.close()

        player = ','.join(playerInfo)
        # print(player)

        playersFile.writelines(player)
        playersFile.write("\n")
        counter += 1

getPlayers(1)
getPlayers(2)
playersFile.close()
