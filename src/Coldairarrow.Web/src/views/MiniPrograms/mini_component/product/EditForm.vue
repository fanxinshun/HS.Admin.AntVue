<template>
  <a-modal :title="title" width="40%" :visible="visible" :confirmLoading="loading" @ok="handleSubmit" @cancel="()=>{this.visible=false}">
    <a-spin :spinning="loading">
      <a-form-model ref="form" :model="entity" :rules="rules" v-bind="layout">
        <a-form-model-item label="上传图片" prop="Image">
          <c-upload-img v-model="entity.Image" :maxCount="1"></c-upload-img>
        </a-form-model-item>
        <a-form-model-item label="链接到" prop="Type">
          <a-input v-model="entity.Type" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="SKU" prop="SKU">
          <a-input v-model="entity.SKU" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="标题" prop="Tittle">
          <a-input v-model="entity.Tittle" autocomplete="off" />
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
        entity: {},
        componentId: '',
        rules: {},
        title: ''
      }
    },
    methods: {
      init() {
        this.visible = true
        this.entity = {}
        this.entity.Component_Id = this.componentId
        this.entity.Image = ''
        this.$nextTick(() => {
          this.$refs['form'].clearValidate()
        })
      },
      openAddForm(componentId, title) {
        this.componentId = componentId
        this.init()
      },
      openEditForm(id, title) {
        this.init()
        if (id) {
          this.loading = true
          this.$http.post('/MiniPrograms/mini_component_item/GetMiniComponentProduct', {
            id: id
          }).then(resJson => {
            this.loading = false
            this.entity = resJson.Data
            this.componentId = ''
          })
        }
      },
      handleSubmit() {
        this.$refs['form'].validate(valid => {
          if (!valid) {
            return
          }
          this.entity.Component_Id = this.entity.Component_Id || this.componentId
          this.loading = true
          this.$http.post('/MiniPrograms/mini_component_item/SaveData', this.entity).then(resJson => {
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
