export function timeFix () {
  const time = new Date()
  const hour = time.getHours()
  return hour < 9 ? '早上好' : hour <= 11 ? '上午好' : hour <= 13 ? '中午好' : hour < 20 ? '下午好' : '晚上好'
}

export function welcome () {
  const arr = ['休息一会儿吧', '准备吃什么呢?', '要不要打一把 DOTA', '我猜你可能累了']
  const index = Math.floor(Math.random() * arr.length)
  return arr[index]
}

/**
 * 触发 window.resize
 */
export function triggerWindowResizeEvent () {
  const event = document.createEvent('HTMLEvents')
  event.initEvent('resize', true, true)
  event.eventType = 'message'
  window.dispatchEvent(event)
}

export function handleScrollHeader (callback) {
  let timer = 0

  let beforeScrollTop = window.pageYOffset
  callback = callback || function () {}
  window.addEventListener(
    'scroll',
    event => {
      clearTimeout(timer)
      timer = setTimeout(() => {
        let direction = 'up'
        const afterScrollTop = window.pageYOffset
        const delta = afterScrollTop - beforeScrollTop
        if (delta === 0) {
          return false
        }
        direction = delta > 0 ? 'down' : 'up'
        callback(direction)
        beforeScrollTop = afterScrollTop
      }, 50)
    },
    false
  )
}

/**
 * Remove loading animate
 * @param id parent element id or class
 * @param timeout
 */
export function removeLoadingAnimate (id = '', timeout = 1500) {
  if (id === '') {
    return
  }
  setTimeout(() => {
    document.body.removeChild(document.getElementById(id))
  }, timeout)
}

/**
 * 拼接完整的图片地址
 * @param src 原地址段
 * @param size 图片请求的像素大小 1~8代表屏幕宽的平分份数大小 >8代表实际像素
 * @returns {*} 返回拼接完整的图片地址 默认是线上
 */
export function imgSrc (src, size) {
  if (!src || !src.trim()) return ''
  // var host = ( (!(base=='http://member.taolaigo.com/handlers')) ? 'aolaigo.com' : 'taolaigo.com');
  var host = (process.env.NODE_ENV === 'development') ? 'taolaigo.com' : 'aolaigo.com'
  var hp = location.protocol
  var http = [hp + '//img1.' + host + '/group1/', hp + '//img2.' + host + '/group1/', hp + '//img3.' + host + '/group1/', hp + '//img4.' + host + '/group1/', hp + '//img5.' + host + '/group1/']
  // http协议头
  var hrefFilter = /^http:|https:/i
  var pick = src.split('.')[0]
  var s = pick.charCodeAt(pick.length - 1) % 5
  if (!hrefFilter.test(src)) {
    src = http[s] + src
  }
  if (size) {
    if (size >= 1 && size <= 8) {
      size = parseInt(window.innerWidth / size * (window.devicePixelRatio || 1), 10)
    }
    size = '=' + size + 'x4096'
    var imgtype = /\.[a-z]{1,4}$/i
    var execResult = imgtype.exec(src)
    if (!execResult) {
      return src
    }
    var suffixer = execResult[0]
    // 去除原有尺寸，并赋值新尺寸
    src = src.replace(new RegExp('(=\\d+x\\d+)?\\' + suffixer), size + suffixer)
  }
  return src
};

