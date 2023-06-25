import subprocess
import pkg_resources
import sys
import json

REQUIRED_PACKAGES = ['vaderSentiment']

for package in REQUIRED_PACKAGES:
    try:
        dist = pkg_resources.get_distribution(package)
        #print('{} ({}) is installed'.format(dist.key, dist.version))
    except pkg_resources.DistributionNotFound:
        #print('{} is NOT installed'.format(package))
        subprocess.call(["pip", "install", package])

from vaderSentiment.vaderSentiment import SentimentIntensityAnalyzer

def sentiment_vader(sentence):
    sid_obj = SentimentIntensityAnalyzer()

    sentiment_dict = sid_obj.polarity_scores(sentence)
    negative = sentiment_dict['neg']
    neutral = sentiment_dict['neu']
    positive = sentiment_dict['pos']
    compound = sentiment_dict['compound']

    if sentiment_dict['compound'] >= 0.05 :
        overall_sentiment = "Positive"

    elif sentiment_dict['compound'] <= - 0.05 :
        overall_sentiment = "Negative"

    else :
        overall_sentiment = "Neutral"
  
    return json.dumps({
        'negative': negative,
        'neutral': neutral,
        'positive': positive,
        'compound': compound,
        'overall_sentiment': overall_sentiment,
    })

# Command-line argument
if __name__ == "__main__":
    sentence = sys.argv[1]
    result = sentiment_vader(sentence)
    print(result)
