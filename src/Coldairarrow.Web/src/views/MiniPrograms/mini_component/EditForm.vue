<template>
  <a-modal :title="title" width="40%" :visible="visible" :confirmLoading="loading" @ok="handleSubmit" @cancel="()=>{this.visible=false}">
    <a-spin :spinning="loading">
      <a-form-model ref="form" :model="entity" :rules="rules" v-bind="layout">
        <a-form-model-item label="组件名称" prop="Sys_Component_Id">
          <c-select v-model="entity.Sys_Component_Id" :disableds="disableds" url="/MiniPrograms/mini_component_type/GetOptionList"
            searchMode="server"></c-select>
        </a-form-model-item>
        <a-form-model-item label="组件描述" prop="Description">
          <a-input v-model="entity.Description" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="序号" prop="Sort">
          <a-input v-model="entity.Sort" autocomplete="off" />
        </a-form-model-item>
      </a-form-model>
    </a-spin>
  </a-modal>
</template>

<script>
  import CSelect from '@/components/CSelect/CSelect'
  import CUploadImg from '@/components/CUploadImg/CUploadImg'
  export default {
    props: {
      parentObj: Object
    },
    components: {
      CSelect,
      CUploadImg
    },
    data() {
      return {
        layout: {
          labelCol: {
            span: 5
          },
          wrapperCol: {
            span: 18
          }
        },
        visible: false,
        loading: false,
        entity: {
          Images: []
        },
        rules: {},
        title: '',
        disableds: true
      }
    },
    methods: {
      init() {
        this.visible = true
        this.entity = {
          Images: []
        }
        this.$nextTick(() => {
          this.$refs['form'].clearValidate()
        })
      },
      openForm(id, title) {
        this.entity.Images = []
        this.init()
        this.disableds = false
        if (id) {
          this.loading = true
          this.$http.post('/MiniPrograms/mini_component/GetMiniComponent', {
            id: id
          }).then(resJson => {
            this.disableds = true
            this.loading = false
            this.entity = resJson.Data
          })
        }
      },
      handleSubmit() {
        this.$refs['form'].validate(valid => {
          if (!valid) {
            return
          }
          this.loading = true
          this.$http.post('/MiniPrograms/mini_component/SaveData', this.entity).then(resJson => {
            this.loading = false
            if (resJson.Success) {
              this.$message.success('操作成功!')
              this.visible = false
              this.parentObj.getDataList()
            } else {
              this.$message.error(resJson.Msg)
            }
          })
        })
      }
    }
  }
</script>
