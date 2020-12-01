<template>
  <div class="user-wrapper">
    <div class="content-box">
      <span>项目地:</span>
      <!-- <a href="https://pro.loacg.com/docs/getting-started" target="_blank">
        <span class="action">
          <a-icon type="question-circle-o"></a-icon>
        </span>
      </a> -->
      <!-- <notice-icon class="action" /> -->
      <a-dropdown>
        <span class="action ant-dropdown-link">
          <a-avatar size="small" icon="user" />
          <span>{{ op().UserName }}</span>
        </span>
        <a-menu slot="overlay">
          <a-menu-item v-for="item in ProjectList" :key="item.Id">{{ item.Text }}
          </a-menu-item>
        </a-menu>
      </a-dropdown>
      <a-dropdown>
        <span class="action ant-dropdown-link user-dropdown-menu">
          <a-avatar size="small" icon="user" />
          <span>{{ op().UserName }}</span>
        </span>
        <a-menu slot="overlay" class="user-dropdown-menu-wrapper">
          <a-menu-item key="1">
            <a href="javascript:;" @click="handleChangePwd()">
              <a-icon type="lock" />
              <span>修改密码</span>
            </a>
            <change-pwd-form ref="changePwd"></change-pwd-form>
          </a-menu-item>
          <a-menu-divider />
          <a-menu-item key="3">
            <a href="javascript:;" @click="handleLogout()">
              <a-icon type="logout" />
              <span>退出登录</span>
            </a>
          </a-menu-item>
        </a-menu>
      </a-dropdown>
    </div>
  </div>
</template>

<script>
  // import NoticeIcon from '@/components/NoticeIcon'
  // import { mapActions, mapGetters } from 'vuex'
  import OperatorCache from '@/utils/cache/OperatorCache'
  import TokenCache from '@/utils/cache/TokenCache'
  import ChangePwdForm from './ChangePwdForm'

  export default {
    name: 'UserMenu',
    components: {
      // NoticeIcon
      ChangePwdForm
    },
    data() {
      return {
        project_id: '0',
        ProjectList: [{
          Id: '0',
          Text: '衣服'
        }, {
          Id: '1',
          Text: '裤子'
        }]
      }
    },
    methods: {
      op() {
        return OperatorCache.info
      },
      // ...mapActions(['Logout']),
      // ...mapGetters(['nickname', 'avatar']),
      handleLogout() {
        const that = this

        this.$confirm({
          title: '提示',
          content: '真的要注销登录吗 ?',
          onOk() {
            TokenCache.deleteToken()
            OperatorCache.clear()
            location.reload()
            // that.$router.push({ path: '/user/login' })
          }
        })
      },
      handleChangePwd() {
        this.$refs.changePwd.open()
      },
      projectSelected() {
        this.project_id = '1'
      }
    }
  }
</script>
