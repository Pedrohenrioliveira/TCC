const https = require('https');
const token = "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";

const options = {
  hostname: 'api.github.com',
  path: '/repos/Pedrohenrioliveira/TCC/issues?state=open&per_page=100',
  method: 'GET',
  headers: {
    'Authorization': 'Bearer ' + token,
    'User-Agent': 'NodeScript',
    'Accept': 'application/vnd.github.v3+json'
  }
};

const req = https.request(options, (res) => {
  let data = '';
  res.on('data', chunk => data += chunk);
  res.on('end', async () => {
    const issues = JSON.parse(data);
    const issuesToClose = issues.filter(i => 
      i.title.includes("Abas de Ligas")
    );
    
    console.log("Encontradas " + issuesToClose.length + " issues para fechar.");

    for (const issue of issuesToClose) {
      await closeIssue(issue.number, issue.title);
    }
  });
});
req.end();

async function closeIssue(number, title) {
  return new Promise((resolve) => {
    const postData = JSON.stringify({ state: 'closed' });
    const patchOptions = {
      hostname: 'api.github.com',
      path: '/repos/Pedrohenrioliveira/TCC/issues/' + number,
      method: 'PATCH',
      headers: {
        'Authorization': 'Bearer ' + token,
        'User-Agent': 'NodeScript',
        'Accept': 'application/vnd.github.v3+json',
        'Content-Type': 'application/json',
        'Content-Length': Buffer.byteLength(postData)
      }
    };
    
    const req = https.request(patchOptions, (res) => {
      res.on('data', () => {}); // Consome os dados para liberar a memória
      res.on('end', () => {
        console.log('✅ Fechada: ' + title);
        resolve();
      });
    });
    req.write(postData);
    req.end();
  });
}
